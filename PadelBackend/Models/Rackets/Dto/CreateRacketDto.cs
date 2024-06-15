using System.ComponentModel.DataAnnotations;

namespace PadelBackend.Models.Rackets.Dto
{
    public class CreateRacketDto
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = null!;

        [MaxLength(255)]
        public string? Description { get; set; } = null!;
        [MaxLength(255)]

        public float Prize { get; set; }

        [Required]
        public RacketCategory Category { get; set; }
    }
}
