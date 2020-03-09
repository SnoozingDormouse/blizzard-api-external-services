using System;
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

namespace BlizzardAPIExternalMetaDataRetriever.Achievements
{
    public class CriteriaService : BlizzardProfileAPIService, ICriteriaService
    {
        private readonly string _baseCharacter = "khoria";
        private readonly string _baseRealm = "moonglade";

        public string _baseCriteriaPath;
        IAchievementContext _achievementContext;

        public CriteriaService(
            IAchievementContext achievementContext,
            IHttpClientFactory clientFactory, 
            string clientId, 
            string clientSecret)
            : base(clientFactory, clientId, clientSecret)
        {
            _baseCriteriaPath = "profile/wow/character/{0}/{1}/achievements";
            _achievementContext = achievementContext;
        }

        public string UpdateAll()
        {
            try
            {
                ClearCriteria();

                var response = GetBlizzardAPIResponseAsJson(CreateQueryURL());
                var achievementsWithCriteria =
                    JsonConvert.DeserializeObject<incoming::AchievementWrapper>(response)
                    .achievements
                    .Take(100);

                var criteria = FlattenCriteria(achievementsWithCriteria);

                //_achievementContext.Categories.AddRange(categories);
                //_achievementContext.Achievements.AddRange(entityAchievements);
                //_achievementContext.SaveChanges();

                return response;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString() + ex.StackTrace.ToString();
            }
        }

        public string RetrieveCriteria()
        {
            return GetBlizzardAPIResponseAsJson(CreateQueryURL());
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
            var EntityCriteria = new List<FlattenedCriteria>();

            foreach (var achievement in achievementsWithCriteria)
            {
                var achievementId = achievement.id;
                int? parentId = null;

                EntityCriteria.Add(new incoming::FlattenedCriteria
                {
                    achievementId = achievementId,
                    parentId = parentId,
                    id = achievement.criteria.id,
                    amount = achievement.criteria.amount,
                    is_completed = achievement.criteria.is_completed
                });

                var firstLevelParentId = achievement.criteria.id;

                if (achievement.criteria.child_criteria != null)
                {
                    foreach (var child in achievement.criteria.child_criteria)
                    {
                        EntityCriteria.Add(new incoming::FlattenedCriteria
                        {
                            achievementId = achievementId,
                            parentId = firstLevelParentId,
                            id = child.id,
                            amount = child.amount,
                            is_completed = child.is_completed
                        });

                        if (child.child_criteria != null)
                        {
                            var secondLevelParentId = child.id;

                            foreach (var grandchild in child.child_criteria)
                            {
                                EntityCriteria.Add(new incoming::FlattenedCriteria
                                {
                                    achievementId = achievementId,
                                    parentId = secondLevelParentId,
                                    id = grandchild.id,
                                    amount = grandchild.amount,
                                    is_completed = grandchild.is_completed
                                });

                                if (grandchild.child_criteria != null)
                                {
                                    var thirdLevelParentId = grandchild.id;

                                    foreach (var greatgrandchild in grandchild.child_criteria)
                                    {
                                        EntityCriteria.Add(new incoming::FlattenedCriteria
                                        {
                                            achievementId = achievementId,
                                            parentId = thirdLevelParentId,
                                            id = greatgrandchild.id,
                                            amount = greatgrandchild.amount,
                                            is_completed = greatgrandchild.is_completed
                                        });
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return EntityCriteria;
        }

        private void ClearCriteria()
        {
            _achievementContext.Criteria.RemoveRange(_achievementContext.Criteria);
            _achievementContext.SaveChanges();
        }

        private string CreateQueryURL()
        {
            return CreateQueryURL(_baseRealm, _baseCharacter);
        }

        private string CreateQueryURL(string realm, string name)
        {
            return String.Format(_baseCriteriaPath, realm, name);
        }
    }
} 
