using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Configuration;
using Pronia.Contexts;
using Pronia.Models;
using Pronia.ViewModels;

namespace Pronia.ViewComponents
{
    public class HeaderViewComponent:ViewComponent
    {
        private readonly ProniaDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public HeaderViewComponent(ProniaDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IViewComponentResult>InvokeAsync()
        {
            HeaderViewModel headerViewModel = new HeaderViewModel();
          
           
            if(User.Identity.IsAuthenticated) 
            {

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var basketItems=await _context.BasketItems.Where(b=>b.AppUserId==user.Id).ToListAsync();

                headerViewModel.BasketItems = basketItems;
                headerViewModel.TotalPrice= basketItems.Sum(b=>b.Product.Price*b.Count);
                headerViewModel.TotalCount = basketItems.Sum(b => b.Count);
            }
            var settings =await _context.Settings.ToDictionaryAsync(x=>x.Key,x=>x.Value);

            headerViewModel.Settings = settings;

            

            return View(headerViewModel);  
        }
    }
}
