using System.ComponentModel.DataAnnotations;

namespace Pronia.Models
{
    public class Shipping
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        [MaxLength(50)]
        public string Description { get; set; } = null!;
        [MaxLength(100)]
        public string Image { get; set; }
    }
}
