using System;

namespace BlizzardData.Domain.Entities
{
    public class CharacterCriteria
    {
        public UInt64 CharacterId { get; set; }
        public int CriteriaId { get; set; }
        public UInt64 Amount { get; set; }
        public bool IsCompleted { get; set; }
    }
}
