using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PadelBackend.Models.Racket.Dto
{
    public class RacketDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;
        [MaxLength(255)]
        public string Description { get; set; } = null!;
        [MaxLength(20)]
        public float Price { get; set; }
        [Required]
        public RacketCategory Category { get; set; }
        [Required]
        public string Image { get; set; } = null!;
    }
}
