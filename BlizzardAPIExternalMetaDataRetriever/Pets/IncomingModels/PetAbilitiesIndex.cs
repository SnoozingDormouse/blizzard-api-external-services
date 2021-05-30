using System.Collections.Generic;
using Newtonsoft.Json;

namespace BlizzardAPIExternalMetaDataRetriever.Pets.IncomingModels
{
    public class PetAbilitiesIndex
    {
        [JsonProperty("abilities")]
        public IEnumerable<Pet> Abilities { get; set; }

        public class Pet
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("id")]
            public int Id { get; set; }
        }
    }
}
