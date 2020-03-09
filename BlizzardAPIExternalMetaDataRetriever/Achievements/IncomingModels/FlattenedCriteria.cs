using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlizzardAPIExternalMetaDataRetriever.Achievements.IncomingModels
{
    public class FlattenedCriteria
    {
        public int achievementId { get; set; }
        public int? parentId { get; set; }
        public int id { get; set; }
        public int amount { get; set; }
        public bool is_completed { get; set; }
    }
}
