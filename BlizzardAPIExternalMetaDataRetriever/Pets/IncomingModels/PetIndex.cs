using System.Collections.Generic;
using Newtonsoft.Json;

namespace BlizzardAPIExternalMetaDataRetriever.Pets.IncomingModels
{
    public class PetIndex
    {
        [JsonProperty("pets")]
        public IEnumerable<Pet> Pets { get; set; }

        public class Pet
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("id")]
            public int Id { get; set; }
        }
    }
}
