using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.Contexts;

namespace Pronia.ViewComponents;

public class ProductViewComponent : ViewComponent
{
    private readonly ProniaDbContext _context;

    public ProductViewComponent(ProniaDbContext context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var products = await _context.Products.Where(p => !p.IsDeleted).Take(8).ToListAsync();
        return View(products);
    }

}
