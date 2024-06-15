namespace PadelBackend.Models.Rackets.Dto
{
    public class RacketDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public float Prize { get; set; }

        public RacketCategory Category { get; set; }
    }
}
