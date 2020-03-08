using BlizzardData.Domain.NonEntities;

namespace BlizzardData.Domain.Entities
{
    public class Criteria
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
    }
}