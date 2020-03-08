using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using incoming = BlizzardAPIExternalMetaDataRetriever.Achievements.IncomingModels;
using BlizzardAPIExternalMetaDataRetriever.Services.BlizzardAPIServices;
using BlizzardData.Data;
using entities = BlizzardData.Domain.Entities;
using Newtonsoft.Json;

namespace BlizzardAPIExternalMetaDataRetriever.Achievements
{
    public class AchievementService : BlizzardAPIService, IAchievementService
    {
        public string _indexApiPath;
        public string _achievementPath;
        AchievementContext _achievementContext;

        public AchievementService(
            AchievementContext achievementContext,
            IHttpClientFactory clientFactory, 
            string clientId, 
            string clientSecret)
            : base(clientFactory, clientId, clientSecret)
        {
            _indexApiPath = "data/wow/achievement/index";
            _achievementPath = "/data/wow/achievement/{0}";
            _achievementContext = achievementContext;
        }

        public string UpdateAllAchievements()
        {
            try
            {
                ClearAllAchievements();

                var response = GetBlizzardAPIResponseAsJson(_indexApiPath);
                var achievementIds = JsonConvert.DeserializeObject<incoming::AchievementWrapper>(response).achievements.Select(a => a.id);

                var achievements = new List<incoming::Achievement>();

                foreach (int id in achievementIds)
                {
                    response = GetBlizzardAPIResponseAsJson(String.Format(_achievementPath, id));
                    var achievement = JsonConvert.DeserializeObject<incoming::Achievement>(response);
                    achievements.Add(achievement);
                }

                //var cata = achievements.Select(a => a.Category);
                //var catb = cata.GroupBy(c => c.Id);
                //var catc = catb.Select(i => i.FirstOrDefault());
                //var catd = catc.ToList();

                //var crita = achievements.Select(a => a.Criteria);
                //var critb = crita.GroupBy(c => c.Id);
                //var critc = critb.Select(i => i.FirstOrDefault());
                //var critd = critc.ToList();

                var categories = achievements.Select(a => a.category).Where(c => c != null).GroupBy(c => c.Id).Select(i => i.FirstOrDefault()).ToList();
                var criteria = achievements.Select(a => a.criteria).Where(c => c != null).GroupBy(c => c.Id).Select(i => i.FirstOrDefault()).ToList();
                var entityAchievements =
                    achievements
                    .Select(a =>
                        new entities::Achievement
                        {
                            Id = a.id,
                            CategoryId = a.category?.Id,
                            Name = a.name,
                            Description = a.description,
                            Points = a.points,
                            IsAccountWide = a.is_account_wide,
                            CriteriaId = a.criteria?.Id
                        });

                _achievementContext.Categories.AddRange(categories);
                _achievementContext.Criteria.AddRange(criteria);
                _achievementContext.Achievements.AddRange(entityAchievements);
                _achievementContext.SaveChanges();

                return response;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString() + ex.StackTrace.ToString();
            }
        }

        private void ClearAllAchievements()
        {
            _achievementContext.Criteria.RemoveRange(_achievementContext.Criteria);
            _achievementContext.Categories.RemoveRange(_achievementContext.Categories);
            _achievementContext.Achievements.RemoveRange(_achievementContext.Achievements);
            _achievementContext.SaveChanges();
        }
    }
} 
