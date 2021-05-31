using System.Collections.Generic;

namespace BlizzardAPIExternalMetaDataRetriever.Pets
{
    public class Pet
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TypeId { get; set; }

        public string TypeName { get; set; }

        public string Description { get; set; }

        public bool IsCapturable { get; set; }

        public bool IsTradable { get; set; }

        public bool IsBattlePet { get; set; }

        public bool IsAllianceOnly { get; set; }

        public bool IsHordeOnly { get; set; }

        public PetSource Source { get; set; }

        public IEnumerable<PetAbility> Abilities { get; set; }

        public CreatureLink Creature { get; set; }

        public class PetSource
        {
            public string Type { get; set; }

            public string Name { get; set; }
        }

        public class PetAbility
        {
            public int Id { get; set; }

            public int Slot { get; set; }

            public string Name { get; set; }

            public int RequiredLevel { get; set; }
        }

        public class CreatureLink
        {
            public string Name { get; set; }

            public int CreatureLinkId { get; set; }
        }
    }
}