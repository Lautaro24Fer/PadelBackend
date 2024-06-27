using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PadelBackend.Models.Racket
{
    public class Racket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Model { get; set; } = null!;
        [Required]
        [MaxLength(50)]
        public string Provider { get; set; } = null!;
        [MaxLength(20)]
        public float Price { get; set; }
        [Required]
        [MaxLength(20)]
        public string Brand { get; set; } = null!;
        [Required]
        public string Image { get; set; } = null!;
    }
}