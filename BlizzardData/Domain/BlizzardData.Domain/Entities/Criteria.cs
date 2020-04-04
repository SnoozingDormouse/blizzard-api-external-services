﻿using System;

namespace BlizzardData.Domain.Entities
{
    public class Criteria
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public int AchievementId { get; set; }
        public string Description { get; set; }
        public UInt64 Amount { get; set; }
    }
}