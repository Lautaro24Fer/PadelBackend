using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PadelBackend.Models.Rackets
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
        public string Title { get; set; } = null!;

        [MaxLength(255)]
        public string Description { get; set; } = null!;
        [MaxLength(255)]

        public float Prize { get; set; }

        [Required]
        public RacketCategory Category { get; set; }

        //Es necesario ver como hacemos las fotos si hace falta agregarlas acá
    }
}
