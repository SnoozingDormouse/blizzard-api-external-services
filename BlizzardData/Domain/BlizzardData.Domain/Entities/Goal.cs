using System;
using System.Collections.Generic;
using System.Text;

namespace BlizzardData.Domain.Entities
{
    public class Goal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentGoalId { get; set; }
    }
}
