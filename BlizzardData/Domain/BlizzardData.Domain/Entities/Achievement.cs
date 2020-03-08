namespace BlizzardData.Domain.Entities
{
        public class Achievement
        {
            public int Id { get; set; }
            public int? CategoryId { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public int Points { get; set; }
            public bool IsAccountWide { get; set; }
            public int? CriteriaId { get; set; }
    }
}
