using System;
using System.Collections.Generic;
using incoming = BlizzardAPIExternalMetaDataRetriever.Achievements.IncomingModels;

namespace BlizzardAPIExternalMetaDataRetriever.Achievements.IncomingModels
{
    public class Criteria
    {
#pragma warning disable IDE1006 // Naming Styles
        public int id { get; set; }
        public UInt64 amount { get; set; }
        public bool is_completed { get; set; }
        public IEnumerable<incoming::Criteria> child_criteria { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
