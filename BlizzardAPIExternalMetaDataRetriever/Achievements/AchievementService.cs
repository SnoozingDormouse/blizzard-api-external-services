using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BlizzardAPIExternalMetaDataRetriever.Achievements.IncomingModels;
using BlizzardAPIExternalMetaDataRetriever.Services.BlizzardAPIServices;
using BlizzardAPIExternalMetaDataRetriever.Shared.Models;
using BlizzardData.Data;
using Newtonsoft.Json;
using entities = BlizzardData.Domain.Entities;

namespace BlizzardAPIExternalMetaDataRetriever.Achievements
{
    public class AchievementService : IAchievementService
    {
        private readonly IDataContext _dataContext;
        private readonly IBlizzardAPIService _blizzardAPIService;
        private readonly string _indexApiPath;
        private readonly string _achievementPath;

        public AchievementService(
            IDataContext dataContext,
            IBlizzardAPIService blizzardAPIService)
        {
            _indexApiPath = "data/wow/achievement/index";
            _achievementPath = "data/wow/achievement/{0}";
            _dataContext = dataContext;
            _blizzardAPIService = blizzardAPIService;
        }

        public async Task<string> Update(int id)
        {
            string result;
            try
            {
                await GetAndPersistAchievement(id);
                result = "Ok";
            }
            catch (Exception ex)
            {
                result = ex.Message + ex.StackTrace;
            }

            return result;
        }

        private async Task GetAndPersistAchievement(int id)
        {
            var achievement = await GetAchievement(id);
            TransformAndStore(achievement);
        }

        public async Task<string> UpdateAll()
        {
            Stopwatch stopwatch = new();
            int currentAchievementId = -1;
            stopwatch.Start();

            var response = await _blizzardAPIService.GetBlizzardAPIResponseAsJsonAsync(_indexApiPath);
            var achievementIds = JsonConvert.DeserializeObject<AchievementsIndex>(response).Achievements.Select(a => a.Id);

            foreach(var id in achievementIds)
            {
                try
                {
                    currentAchievementId = id;
                    await GetAndPersistAchievement(id);
                }
                catch (HttpRequestException)
                {
                    // retry
                    await GetAndPersistAchievement(currentAchievementId);
                    continue;
                }
                catch (Exception ex)
                {
                    stopwatch.Stop();

                    return
                        String.Format("After {0:hh\\:mm\\:ss} \r\n", stopwatch.Elapsed) +
                        String.Format("An error occurred when retrieving and persisting achievement {0}\r\n", currentAchievementId)
                        + ex.Message.ToString()
                        + ex.StackTrace.ToString();
                }
            }

            int numAchievements = achievementIds.Count();
            stopwatch.Stop();

            return String.Format("{0} achievements downloaded in {1:hh\\:mm\\:ss}", achievementIds.Count(), stopwatch.Elapsed);
            
        }

        internal async Task<Achievement> GetAchievement(int id)
        {
            var response = 
                await _blizzardAPIService
                    .GetBlizzardAPIResponseAsJsonAsync(String.Format(_achievementPath, id));

            return 
                JsonConvert.DeserializeObject<Achievement>(response);
        }

        internal void TransformAndStore(Achievement achievement)
        {
            TransformAndPersistAchievement(achievement);
            if (achievement.Category != null)
                TransformAndPersistCategory(achievement.Category);

            if (achievement.Criteria != null)
            {
                TransformAndPersistCriteria(achievement);
                TransformAndPersistChildCriteria(achievement);
            }

            _dataContext.SaveChanges();
        }

        private void TransformAndPersistAchievement(Achievement achievement)
        {
            var currentAchievement = _dataContext.Achievements.Where(a => a.Id == achievement.Id).SingleOrDefault();

            if (currentAchievement == null)
            {
                _dataContext.Achievements.Add(
                    new entities::Achievement
                    {
                        Id = achievement.Id,
                        CategoryId = achievement.Category.Id,
                        Name = achievement.Name,
                        Description = achievement.Description,
                        Points = achievement.Points,
                        IsAccountWide = achievement.IsAccountWide,
                        RewardDescription = achievement.RewardDescription
                    });
            }
            else
            {
                currentAchievement.CategoryId = achievement.Category.Id;
                currentAchievement.Name = achievement.Name;
                currentAchievement.Description = achievement.Description;
                currentAchievement.Points = achievement.Points;
                currentAchievement.IsAccountWide = achievement.IsAccountWide;
                currentAchievement.RewardDescription = achievement.RewardDescription;
            }
        }

        private void TransformAndPersistCategory(KeyNameId category)
        {
            var currentCategory = _dataContext.Categories.Where(c => c.Id == category.Id).SingleOrDefault();

            if (currentCategory == null)
            {
                _dataContext.Categories.Add(
                    new entities::Category
                    {
                        Id = category.Id,
                        Name = category.Name,
                    });
            }
            else
            {
                currentCategory.Id = category.Id;
                currentCategory.Name = category.Name;
            }
        }

        private void TransformAndPersistCriteria(Achievement achievement)
        {
            var criteria = achievement.Criteria;
            PersistCriteria(criteria);

            RegisterCriteriaWithAchievement(achievement.Id, criteria.Id);
        }

        private void PersistCriteria(Criteria criteria)
        {
            var currentCriteria = _dataContext.Criteria.Where(c => c.Id == criteria.Id).SingleOrDefault();

            if (currentCriteria == null)
            {
                _dataContext.Criteria.Add(
                    new entities::Criteria
                    {
                        Id = criteria.Id,
                        Description = criteria.Description,
                        Amount = criteria.Amount,
                        OperatorType = criteria.MustComplete?.Type,
                        OperatorName = criteria.MustComplete?.Name,
                        AchievementId = criteria.Achievement?.Id
                    });
            }
            else
            {
                currentCriteria.Id = criteria.Id;
                currentCriteria.Description = criteria.Description;
                currentCriteria.Amount = criteria.Amount;
                currentCriteria.OperatorType = criteria.MustComplete?.Type;
                currentCriteria.OperatorName = criteria.MustComplete?.Name;
                currentCriteria.AchievementId = criteria.Achievement?.Id;
            }
        }

        private void TransformAndPersistChildCriteria(Achievement achievement)
        {
            var childCriteria = achievement.Criteria.ChildCriteria;

            if (childCriteria != null)
            {
                var anchorAchievementId = achievement.Id;
                var parentCriteriaId = achievement.Criteria.Id;

                foreach (ChildCriteria criteria in childCriteria)
                    AddChildCriteria(criteria, anchorAchievementId, parentCriteriaId);
            }
        }

        private void AddChildCriteria(
            ChildCriteria criteria,
            int anchorAchievementId,
            int parentCriteriaId)
        {
            RegisterCriteriaWithAchievement(anchorAchievementId, criteria.Id);
            RegisterCriteriaWithCriteria(parentCriteriaId, criteria.Id);
            PersistChildCriteria(criteria);

            if (criteria.SubCriteria != null)
            {
                foreach (ChildCriteria childCriteria in criteria.SubCriteria)
                    AddChildCriteria(childCriteria, anchorAchievementId, criteria.Id);
            }
        }

        private void PersistChildCriteria(ChildCriteria criteria)
        {
            var currentCriteria = _dataContext.Criteria.Where(c => c.Id == criteria.Id).SingleOrDefault();
            
            entities::PlayerFaction? playerFaction = null;
            if (criteria.Faction?.Name != null)
                playerFaction = (entities::PlayerFaction)Enum.Parse(typeof(entities::PlayerFaction), criteria.Faction.Name);

            if (currentCriteria == null)
            {
                _dataContext.Criteria.Add(
                    new entities::Criteria
                    {
                        Id = criteria.Id,
                        Description = criteria.Description,
                        Amount = criteria.Amount,
                        OperatorType = criteria.MustComplete?.Type,
                        OperatorName = criteria.MustComplete?.Name,
                        Faction = playerFaction,
                        AchievementId = criteria.Achievement?.Id
                    });
            }
            else
            {
                currentCriteria.Id = criteria.Id;
                currentCriteria.Description = criteria.Description;
                currentCriteria.Amount = criteria.Amount;
                currentCriteria.OperatorType = criteria.MustComplete?.Type;
                currentCriteria.OperatorName = criteria.MustComplete?.Name;
                currentCriteria.Faction = playerFaction;
                currentCriteria.AchievementId = criteria.Achievement?.Id;
            }
        }

        private void RegisterCriteriaWithCriteria(int parentCriteriaId, int childCriteriaId)
        {
            var currentCriteriaCriteria =
                            _dataContext
                            .CriteriaCriterias
                            .Where(cc => cc.CriteriaId == parentCriteriaId && cc.ChildCriteriaId == childCriteriaId)
                            .SingleOrDefault();

            if (currentCriteriaCriteria == null)
            {
                _dataContext.CriteriaCriterias.Add(
                    new entities::CriteriaCriteria
                    {
                        CriteriaId = parentCriteriaId,
                        ChildCriteriaId = childCriteriaId
                    });
            }
        }

        private void RegisterCriteriaWithAchievement(int achievementId, int criteriaId)
        {
            var currentAchievementCriteria =
                            _dataContext
                            .AchievementCriterias
                            .Where(ac => ac.AchievementId == achievementId && ac.CriteriaId == criteriaId)
                            .SingleOrDefault();

            if (currentAchievementCriteria == null)
            {
                _dataContext.AchievementCriterias.Add(
                    new entities::AchievementCriteria
                    {
                        AchievementId = achievementId,
                        CriteriaId = criteriaId
                    });
            }
        }
    }
} 
