namespace InforceUrlShortener.Domain.Entities
{
    public class AlgorithmDescription
    {
        public static readonly Guid SingletonId = Guid.Parse("00000000-0000-0000-0000-000000000001");
        public Guid Id { get; set; }
        public string Description { get; set; } = default!;
        public DateTime LastUpdated { get; set; }
    }
}
