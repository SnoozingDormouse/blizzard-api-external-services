namespace BlizzardData.Domain.Entities
{
    public class BattlePet
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public BattlePetFamily Family { get; set; }

        public string Description { get; set; }

        public bool IsCapturable { get; set; }

        public bool IsTradable { get; set; }

        public bool IsBattlePet { get; set; }

        public bool IsAllianceOnly { get; set; }

        public bool IsHordeOnly { get; set; }

        public string SourceType { get; set; }

        public string SourceName { get; set; }
    }
}
