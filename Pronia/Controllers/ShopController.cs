using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.Contexts;
using Pronia.Models;

namespace Pronia.Controllers;

public class ShopController : Controller
{
    private readonly ProniaDbContext _context;
    public ShopController(ProniaDbContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Index()
    {
        int productCount = await _context.Products.Where(p=>!p.IsDeleted).CountAsync();
        ViewBag.ProductCount = productCount;
        return View();
    }
    public async Task<IActionResult>LoadMore(int skip)
    {
        var productCount = await _context.Products.Where(p => !p.IsDeleted).CountAsync();
        if (skip > productCount)
            return BadRequest();

        List<Product> products = await _context.Products
            .Where(p=> !p.IsDeleted)
            .Skip(skip)
            .Take(8)
            .ToListAsync();

        return PartialView("_PartialProduct", products);
    }
    public async Task<IActionResult>ProductDetail(int id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p=> p.Id == id & !p.IsDeleted);
        return PartialView("_ProductModalPartial",product);
    }
}
