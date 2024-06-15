namespace PadelBackend.Models.Rackets.Dto
{
    public class UpdateRacketDto
    {
            public string? Title { get; set; }

            public string? Description { get; set; }

            public float? Prize { get; set; }

            public RacketCategory? Category { get; set; }

    }
}
