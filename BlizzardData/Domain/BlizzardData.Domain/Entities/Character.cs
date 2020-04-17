using System;

namespace BlizzardData.Domain.Entities
{
    public class Character
    {
        public UInt64 UserAccountId { get; set; }
        public string Name { get; set; }
        public string Realm { get; set; }
        public PlayerFaction Faction { get; set; }
        public string CharacterClass { get; set; }
        public string Race { get; set; }
        public int Level { get; set; }
        public UInt64 BlizzardId { get; set; }
        public DateTime LastUpdatedDateTime { get; set; }
    }
}
