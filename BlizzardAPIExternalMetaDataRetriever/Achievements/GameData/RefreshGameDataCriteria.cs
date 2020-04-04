using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlizzardData.Data;
using BlizzardData.Domain.Entities;

namespace BlizzardAPIExternalMetaDataRetriever.Achievements
{
    internal class RefreshGameDataCriteria
    {
        private readonly IAchievementContext _achievementContext;
        private readonly ICriteriaService _criteriaService;

        public RefreshGameDataCriteria(
            IAchievementContext achievementContext,
            ICriteriaService criteriaService)
        {
            _achievementContext = achievementContext;
            _criteriaService = criteriaService;
        }

        public async Task<string> UpdateAll()
        {
            try
            {
                ClearCriteria();
                int saved = await RetrieveAndPersistCriteria();

                return saved.ToString() + " criteria saved to database";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString() + "\r\n" +
                    ex.InnerException.ToString() + "\r\n" +
                    ex.StackTrace.ToString();
            }
        }

        private async Task<int> RetrieveAndPersistCriteria()
        {
            IEnumerable<Criteria> criteria = await _criteriaService.GetCriteriaFromBlizzardAPIAsync();
            int saved = SaveCriteria(criteria);
            return saved;
        }

        private int SaveCriteria(IEnumerable<Criteria> criteria)
        {
            _achievementContext.Criteria.AddRange(criteria);
            return _achievementContext.SaveChanges();
        }

        private void ClearCriteria()
        {
            _achievementContext.Criteria?.RemoveRange(_achievementContext.Criteria);
            _achievementContext.SaveChanges();
        }
    }
}
