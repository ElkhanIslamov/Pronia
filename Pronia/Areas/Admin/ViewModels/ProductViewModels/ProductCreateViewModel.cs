namespace Pronia.Areas.Admin.ViewModels.ProductViewModels
{
    public class ProductCreateViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int DiscountPrice { get; set; }
        public int Rating { get; set; }
        public IFormFile Image { get; set; }
        public int CategoryId { get; set; }

    }
}
