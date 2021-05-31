using Newtonsoft.Json;

namespace BlizzardAPIExternalMetaDataRetriever.Pets.IncomingModels
{
    public class PetAbility
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("battle_pet_type")]
        public PetType BattlePetType { get; set; }

        [JsonProperty("rounds")]
        public int Rounds { get; set; }

        [JsonProperty("cooldown")]
        public int Cooldown { get; set; }

        public class PetType
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }
        }
    }
}
