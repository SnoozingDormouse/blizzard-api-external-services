using System;
using System.Collections.Generic;
using System.Linq;
using incoming = BlizzardAPIExternalMetaDataRetriever.Achievements.IncomingModels;
using BlizzardAPIExternalMetaDataRetriever.Services.BlizzardAPIServices;
using BlizzardData.Data;
using entities = BlizzardData.Domain.Entities;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace BlizzardAPIExternalMetaDataRetriever.Achievements
{
    public class AchievementService : IAchievementService
    {
        private readonly IAchievementContext _achievementContext;
        private readonly IBlizzardAPIService _blizzardAPIService;
        public readonly string _indexApiPath;
        public readonly string _achievementPath;

        public AchievementService(
            IAchievementContext achievementContext,
            IBlizzardAPIService blizzardAPIService)
        {
            _indexApiPath = "data/wow/achievement/index";
            _achievementPath = "/data/wow/achievement/{0}";
            _achievementContext = achievementContext;
            _blizzardAPIService = blizzardAPIService;
        }

        public async Task<string> UpdateAll()
        {
            try
            {
                ClearAllAchievements();

                var response = await _blizzardAPIService.GetBlizzardGameDataAPIResponseAsJsonAsync(_indexApiPath);
                var achievementIds = JsonConvert.DeserializeObject<incoming::AchievementWrapper>(response).achievements.Select(a => a.id);

                var achievements = new List<incoming::Achievement>();

                foreach(var id in achievementIds)
                {
                   response = await _blizzardAPIService.GetBlizzardGameDataAPIResponseAsJsonAsync(String.Format(_achievementPath, id));
                   var achievement = JsonConvert.DeserializeObject<incoming::Achievement>(response);
                   achievements.Add(achievement);
                }

                var categories = achievements.Select(a => a.category).Where(c => c != null).GroupBy(c => c.Id).Select(i => i.FirstOrDefault()).ToList();

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
                            CriteriaId = a.criteria?.id,
                            NextAchievementId = a.next_achievement?.Id
                        });

                _achievementContext.Categories.AddRange(categories);
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
            _achievementContext.Categories?.RemoveRange(_achievementContext.Categories);
            _achievementContext.Achievements?.RemoveRange(_achievementContext.Achievements);
            _achievementContext.SaveChanges();
        }
    }
} 
