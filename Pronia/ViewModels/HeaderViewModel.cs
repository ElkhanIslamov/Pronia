using Pronia.Models;

namespace Pronia.ViewModels
{
    public class HeaderViewModel
    {
        public Dictionary<string,string> Settings { get; set; }
        public List<BasketItem> BasketItems { get; set; }
        public double  TotalPrice { get; set; }
        public int TotalCount { get; set; }
    }
}
