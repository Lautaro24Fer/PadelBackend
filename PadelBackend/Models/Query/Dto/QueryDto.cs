using System.Collections;

namespace PadelBackend.Models.Query.Dto
{
    public class QueryDto
    {
        public int? Limit { get; set; } = 0;
        public string? Model { get; set; } = null!;
        public float? MinPrice { get; set; } = null!;
        public float? MaxPrice { get; set; } = null!;
        public string? Brand { get; set; } = null!;
    }
}
