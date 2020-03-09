using System.Collections.Generic;
using incoming = BlizzardAPIExternalMetaDataRetriever.Achievements.IncomingModels;

namespace BlizzardAPIExternalMetaDataRetriever.Achievements.IncomingModels
{
    public class Criteria
    {
        public int id { get; set; }
        public int amount { get; set; }
        public bool is_completed { get; set; }
        public IEnumerable<incoming::Criteria> child_criteria { get; set; }
    }
}
