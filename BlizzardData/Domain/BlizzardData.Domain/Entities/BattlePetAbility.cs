namespace BlizzardData.Domain.Entities
{
    public class BattlePetAbility
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public BattlePetFamily Family { get; set; }

        public int Rounds { get; set; }

        public int Cooldown { get; set; }
    }
}
