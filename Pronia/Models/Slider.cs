using System.ComponentModel.DataAnnotations;

namespace Pronia.Models
{
    public class Slider
    {
        public int Id { get; set; }

        public int Offer { get; set; } 
        public string Title { get; set; } = null!;
        [MaxLength(50)]
        public string Discription { get; set; } = null!;
        [MaxLength(100)]
        public string Image { get; set; } = null !;
    }
}
