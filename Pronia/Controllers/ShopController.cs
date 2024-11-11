using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.Contexts;
using Pronia.Models;

namespace Pronia.Controllers;

public class ShopController : Controller
{
    private readonly ProniaDbContext _context;
    private readonly UserManager<AppUser> _userManager;
    public ShopController(ProniaDbContext context, UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
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
    [Authorize]
    public async Task<IActionResult>AddProductToBasket(int productId)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p=>p.Id== productId & !p.IsDeleted);  
        if(product == null)
            return NotFound();

        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        var basketItem = await _context.BasketItems.Include(b=>b.Product).FirstOrDefaultAsync(b=>b.ProductId==productId && b.AppUserId==user.Id);
        if(basketItem == null)
        {
        BasketItem newBasketItem = new()
        {
            ProductId = product.Id,
            AppUserId = user.Id,
            Count = 1,
            CreatedTime = DateTime.UtcNow,
        };
        await _context.AddAsync(newBasketItem);
        }
        else
        {
           basketItem.Count++;  
        }
        await _context.SaveChangesAsync();

        var basketItems = await _context.BasketItems.Include(b=>b.Product).Where(b => b.AppUserId == user.Id).ToListAsync();

        return PartialView("_BasketPartial", basketItems);
    }
}
