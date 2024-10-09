using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.Contexts;
using Pronia.ViewModels;

namespace Pronia.Controllers;

public class HomeController : Controller
{
    private readonly ProniaDbContext _context;

    public HomeController(ProniaDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
       var sliders = await _context.Sliders.ToListAsync();
        var shippings = await _context.Shippings.ToListAsync();

        HomeViewModel homeViewModel = new HomeViewModel()
        {
            Sliders = sliders,
            Shippings = shippings
        };

        return View(homeViewModel);
    }

}
