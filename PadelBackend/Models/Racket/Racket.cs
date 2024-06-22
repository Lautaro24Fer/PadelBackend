using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PadelBackend.Models.Racket
{
    public enum RacketCategory
    {
        Diamante,
        Lagrima,
        Redonda
    }
    public class Racket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
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