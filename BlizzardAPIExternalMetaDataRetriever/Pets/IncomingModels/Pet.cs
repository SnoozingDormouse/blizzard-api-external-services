using Newtonsoft.Json;
using System.Collections.Generic;

namespace BlizzardAPIExternalMetaDataRetriever.Pets.IncomingModels
{
    public class Pet
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("battle_pet_type")]
        public PetType BattlePetType { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("is_capturable")]
        public bool IsCapturable { get; set; }

        [JsonProperty("is_tradable")]
        public bool IsTradeable { get; set; }

        [JsonProperty("is_battlepet")]
        public bool IsBattlePet { get; set; }

        [JsonProperty("is_alliance_only")]
        public bool IsAllianceOnly { get; set; }

        [JsonProperty("is_horde_only")]
        public bool IsHordeOnly { get; set; }

        [JsonProperty("abilities")]
        public List<PetAbility> Abilities { get; set; }

        [JsonProperty("source")]
        public PetSource Source { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("creature")]
        public PetCreature Creature { get; set; }

        [JsonProperty("is_random_creature_display")]
        public bool IsRandomCreatureDisplay { get; set; }

        public class PetType
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }
        }

        public class PetAbility
        {
            [JsonProperty("ability")]
            public PetAbilityNameId Ability { get; set; }

            [JsonProperty("slot")]
            public int Slot { get; set; }

            [JsonProperty("required_level")]
            public int RequiredLevel { get; set; }
        }

        public class PetAbilityNameId
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }
        }

        public class PetSource
        {
            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }
        }

        public class PetCreature
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }
        }
    }
}
