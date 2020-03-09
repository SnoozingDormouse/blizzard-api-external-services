﻿using System;
using System.Linq;
using System.Net.Http;
using incoming = BlizzardAPIExternalMetaDataRetriever.Achievements.IncomingModels;
using BlizzardAPIExternalMetaDataRetriever.Services.BlizzardAPIServices;
using BlizzardData.Data;
using entities = BlizzardData.Domain.Entities;
using models = BlizzardData.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using BlizzardAPIExternalMetaDataRetriever.Achievements.IncomingModels;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace BlizzardAPIExternalMetaDataRetriever.Achievements
{
    public class CriteriaService : ICriteriaService
    {
        private IAchievementContext _achievementContext;
        private IBlizzardAPIService _blizzardAPIService;
        private readonly string _pathURL;

        public CriteriaService(
            IAchievementContext achievementContext,
            IBlizzardAPIService blizzardAPIService)
        {
            _achievementContext = achievementContext;
            _blizzardAPIService = blizzardAPIService;
            _pathURL = "achievements";
        }

        public string UpdateAll()
        {
            try
            {
                ClearCriteria();

                var criteria = FlattenCriteria(RetrieveAchievementsWithCriteria());

                var entityCriteria = criteria.Select(c => (entities::Criteria)c);
                _achievementContext.Criteria.AddRange(entityCriteria);
                var saved = _achievementContext.SaveChanges();

                return saved.ToString() + " criteria saved to database";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString() + "\r\n" +
                    ex.InnerException.ToString() + "\r\n" +
                    ex.StackTrace.ToString();
            }
        }

        public IEnumerable<incoming::Achievement> RetrieveAchievementsWithCriteria()
        {
            var response = _blizzardAPIService.GetBlizzardDefaultProfileAPIResponseAsJson(_pathURL);
            
            return
                JsonConvert.DeserializeObject<incoming::AchievementWrapper>(response)
                .achievements;
        }

        public ReadOnlyCollection<models::Criteria> GetAllCriteria(string response)
        {
            var achievementsWithCriteria =
                JsonConvert.DeserializeObject<AchievementWrapper>(response)
                .achievements;

            var criteria = FlattenCriteria(achievementsWithCriteria);

            return new ReadOnlyCollection<models.Criteria>(
                criteria
                .Select(c => new models.Criteria
                    { 
                        Id = c.id,
                        ParentId = c.parentId,
                        AchievementId = c.achievementId,
                        Description = null,                 // TODO: this comes from the GameDataAPI so should really call both and collate
                        Amount = c.amount,
                        IsCompleted = c.is_completed
                    })
                .ToList());
        }

        private List<FlattenedCriteria> FlattenCriteria(IEnumerable<Achievement> achievementsWithCriteria)
        {
            _criteriaList = new List<FlattenedCriteria>();

            foreach (var achievement in achievementsWithCriteria)
            {
                var achievementId = achievement.id;
                int? parentId = null;

                if (achievement.criteria != null)
                    AddToEntitity(achievementId, achievement.criteria, parentId);
            }

            return _criteriaList;
        }

        List<FlattenedCriteria> _criteriaList;

        private void AddToEntitity(int achievementId, Criteria criteria, int? parentId)
        {
            _criteriaList.Add(new incoming::FlattenedCriteria
            {
                achievementId = achievementId,
                parentId = parentId,
                id = criteria.id,
                amount = criteria.amount,
                is_completed = criteria.is_completed
            });

            if (criteria.child_criteria != null)
            {
                Debug.WriteLine("requesting drilling down for {0} criteria", criteria.child_criteria.Count());
                foreach (var childCriteria in criteria.child_criteria)
                {
                    AddToEntitity(achievementId, childCriteria, criteria.id);
                }
            }
        }

        private void ClearCriteria()
        {
            _achievementContext.Criteria.RemoveRange(_achievementContext.Criteria);
            _achievementContext.SaveChanges();
        }
    }
} 
