using System.Collections.Generic;
using BlizzardData.Domain.Entities;

namespace BlizzardAPIExternalMetaDataRetriever.Achievements.IncomingModels
{
    public class CriteriaWrapper
    {
        public IEnumerable<Criteria> criteria { get; set; }
    }
}
