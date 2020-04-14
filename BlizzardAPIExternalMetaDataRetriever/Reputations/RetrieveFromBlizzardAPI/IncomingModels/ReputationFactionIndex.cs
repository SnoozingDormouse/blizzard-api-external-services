using System.Collections.Generic;

namespace BlizzardAPIExternalMetaDataRetriever.Reputations.RetrieveFromBlizzardAPI.IncomingModels
{
    public class ReputationFactionIndex
    {
        public IEnumerable<ReputationFaction> factions { get; set; }
        public IEnumerable<ReputationFaction> root_factions { get; set; }
    }
}
