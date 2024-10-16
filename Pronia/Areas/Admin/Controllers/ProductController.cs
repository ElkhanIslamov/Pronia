﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.Areas.Admin.ViewModels.ProductViewModels;
using Pronia.Contexts;
using Pronia.Helpers.Extensions;
using Pronia.Models;

namespace Pronia.Areas.Admin.Controllers;
[Area("Admin")]
<<<<<<< HEAD
[Authorize(Roles = "Admin")]
=======
[Authorize(Roles ="Admin,Moderator")]
>>>>>>> 7da07ac27062cb0bdbab6832752d563215280c78
public class ProductController : Controller
{
    private readonly ProniaDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ProductController(ProniaDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var product = await _context.Products
            .AsNoTracking()
            .Include(p=>p.Category)
            .ToListAsync();

        return View(product);
    }
    [Authorize(Roles ="User")]
    public async Task<IActionResult> Create()
    {
        ViewBag.Categories = await _context.Categories.ToListAsync(); 
        return View(); 
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductCreateViewModel product)
    {
        ViewBag.Categories = await _context.Categories.ToListAsync();
<<<<<<< HEAD

        if (product.Image.CheckFileSize(3000))
        {
            ModelState.AddModelError("Image", "File size limit exceeded");
        }
        if(product.Image.CheckFileType("image/"))
        {
            ModelState.AddModelError("Image", "File type Must be image file");
=======
        if(product.Image.CheckFileSize(3000))
        {
            ModelState.AddModelError("Image", "File Size Limit Exceeded");
        }
        if(product.Image.CheckFileType("iamge/"))
        {
            ModelState.AddModelError("Image", "File type must be jpg");
>>>>>>> 57991215b1568482bb7c3fa294a1a116d2b1b740
        }
        string fileName = $"{Guid.NewGuid()}-{product.Image.FileName}";
        string path = Path.Combine(_webHostEnvironment.WebRootPath,"assets","img","product",fileName);
        using(FileStream stream = new FileStream(path,FileMode.Create))
        {
            await product.Image.CopyToAsync(stream);
        }
        Product newProduct = new()
        {
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Rating = product.Rating,
            Image = fileName,
            DiscountPrice = product.DiscountPrice,
            CategoryId = product.CategoryId
        };
        await _context.Products.AddAsync(newProduct);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));

    }
    public async Task<IActionResult>Update(int id)
    {
        var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p=>p.Id==id & !p.IsDeleted);
        if (product == null)    
            return NotFound();

        ViewBag.Categories = await _context.Categories.ToListAsync();

        ProductUpdateViewModel productUpdateViewModel = new()
        {
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Rating = product.Rating,
            Image = product.Image,
            DiscountPrice = product.DiscountPrice,
            CategoryId = product.CategoryId
           
        };


        return View(productUpdateViewModel);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult>Update(int id,ProductUpdateViewModel productUpdateViewModel)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p=>p.Id== id & !p.IsDeleted); 
        if(product == null)
            return NotFound();

        product.Name = productUpdateViewModel.Name;
        product.Description = productUpdateViewModel.Description;
        product.Price = productUpdateViewModel.Price;
        product.Rating = productUpdateViewModel.Rating;
        product.Image = productUpdateViewModel.Image;
        product.UpdatedTime = DateTime.UtcNow;
        product.DiscountPrice = productUpdateViewModel.DiscountPrice;

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult>Delete(int id)
    {
        var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p=>p.Id== id & !p.IsDeleted);
        if(product == null) return NotFound();

        return View(product);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    [ActionName(nameof(Delete))]
    public async Task<IActionResult>DeleteProduct(int id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p=>p.Id== id & !p.IsDeleted); 
        if(product == null) return NotFound();
         _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));

    }
   
   
}
