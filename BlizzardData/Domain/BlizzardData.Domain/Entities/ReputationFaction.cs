namespace BlizzardData.Domain.Entities
{
    public class ReputationFaction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? CanParagon { get; set; }
        public int ReputationTiers { get; set; }
        public PlayerFaction PlayerFaction { get; set; }
    }
}
