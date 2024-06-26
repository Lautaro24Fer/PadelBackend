using System.ComponentModel.DataAnnotations;

namespace PadelBackend.Models.Racket.Dto
{
    public class RacketsDto
    {
        public int Id { get; set; }
        public string Model { get; set; } = null!;
        public float Price { get; set; }
        public string Brand { get; set; } = null!;
        public string Image { get; set; } = null!;
    }
}
