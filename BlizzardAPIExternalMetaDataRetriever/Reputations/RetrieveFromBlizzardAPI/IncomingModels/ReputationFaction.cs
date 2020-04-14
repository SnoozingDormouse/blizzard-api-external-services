using BlizzardAPIExternalMetaDataRetriever.Shared.Models;

namespace BlizzardAPIExternalMetaDataRetriever.Reputations.RetrieveFromBlizzardAPI.IncomingModels
{
    public class ReputationFaction
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool? can_paragon { get; set; }
        public ReputationTiers reputation_tiers { get; set; }
        public TypeName player_faction { get; set; }
    }
}
