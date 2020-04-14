using System;

namespace BlizzardData.Domain.Entities
{
    public class CharacterAchievement
    {
        public UInt64 CharacterId { get; set; }
        public int AchievementId { get; set; }
        public UInt64 CompletedTimestamp { get; set; }
    }
}
