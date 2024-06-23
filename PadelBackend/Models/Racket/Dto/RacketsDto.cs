using System.ComponentModel.DataAnnotations;

namespace PadelBackend.Models.Racket.Dto
{
    public class RacketsDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;
        [MaxLength(255)]
        public string Description { get; set; } = null!;
        [MaxLength(20)]
        public float Price { get; set; }
        [Required]
        public string Category { get; set; } = null!;
        [Required]
        public string Image { get; set; } = null!;
    }
}
