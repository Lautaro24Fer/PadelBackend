using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PadelBackend.Models.Racket
{
    public static class RacketCategories
    {
        public static readonly List<string> categories = new List<string>
        {
        "diamante",
        "lagrima",
        "redonda"
        };
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
        public string Category { get; set; } = null!;
        [Required]
        public string Image { get; set; } = null!;
    }
}