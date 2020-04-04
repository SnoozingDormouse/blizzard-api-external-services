﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BlizzardData.Domain.Entities
{
    public class CharacterCriteria
    {
        public int CharacterId { get; set; }
        public int CriteriaId { get; set; }
        public UInt64 Amount { get; set; }
        public bool IsComplete { get; set; }
    }
}