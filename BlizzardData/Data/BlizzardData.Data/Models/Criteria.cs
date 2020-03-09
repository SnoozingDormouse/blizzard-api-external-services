namespace BlizzardData.Models
{
    public class Criteria
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public int AchievementId { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public bool IsCompleted { get; set; }
    }
}