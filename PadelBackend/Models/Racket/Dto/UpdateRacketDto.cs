using System.ComponentModel.DataAnnotations;

namespace PadelBackend.Models.Racket.Dto
{
    public class UpdateRacketDto
    {
        [MaxLength(100)]
        public string? Name { get; set; }
        [MaxLength(255)]
        public string? Description { get; set; }
        [MaxLength(20)]
        public float? Price { get; set; }
        public string Category { get; set; } = null!;
        public string? Image { get; set; }
    }
}
