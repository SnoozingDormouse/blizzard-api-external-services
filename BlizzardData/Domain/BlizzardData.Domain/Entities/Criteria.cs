using System;

namespace BlizzardData.Domain.Entities
{
    public class Criteria
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public UInt64 Amount { get; set; }
        public string OperatorType { get; set; }
        public string OperatorName { get; set; }
        public PlayerFaction? Faction { get; set; }
        public int? AchievementId { get; set; }
    }
}