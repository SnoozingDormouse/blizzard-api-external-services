using System;
using entities = BlizzardData.Domain.Entities;

namespace BlizzardAPIExternalMetaDataRetriever.Achievements.IncomingModels
{
    public class FlattenedCriteria
    {
#pragma warning disable IDE1006 // Naming Styles
        public int achievementId { get; set; }
        public int? parentId { get; set; }
        public int id { get; set; }
        public UInt64 amount { get; set; }
        public bool is_completed { get; set; }
#pragma warning restore IDE1006 // Naming Styles

        public static explicit operator entities.Criteria(FlattenedCriteria c)
        {
            if (c == null)
                throw new ArgumentNullException();

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
