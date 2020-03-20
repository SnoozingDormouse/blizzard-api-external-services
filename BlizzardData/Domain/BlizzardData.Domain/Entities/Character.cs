using System;
using System.Collections.Generic;
using System.Text;

namespace BlizzardData.Domain.Entities
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Realm { get; set; }
        public int Level { get; set; }
        public int BlizzardId { get; set; }
    }
}
