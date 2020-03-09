using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using entities = BlizzardData.Domain.Entities;
using models = BlizzardData.Models;

namespace BlizzardAPIExternalMetaDataRetriever.Achievements.IncomingModels
{
    public class FlattenedCriteria
    {
        public int achievementId { get; set; }
        public int? parentId { get; set; }
        public int id { get; set; }
        public int amount { get; set; }
        public bool is_completed { get; set; }

        public static explicit operator entities.Criteria(FlattenedCriteria c)
        {
            return
                new entities.Criteria
                {
                    Id = c.id,
                    ParentId = c.parentId,
                    AchievementId = c.achievementId,
                    Amount = c.amount,
                };
        }
    }
}
