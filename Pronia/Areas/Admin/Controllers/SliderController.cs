using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.Contexts;
using Pronia.Models;

namespace Pronia.Areas.Admin.Controllers;
[Area("Admin")]
public class SliderController : Controller
{
    private readonly ProniaDbContext _context;

    public SliderController(ProniaDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var sliders = await _context.Sliders.ToListAsync();
        return View(sliders);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(Slider slider) 
    {
       await  _context.Sliders.AddAsync(slider);
       await _context.SaveChangesAsync(); 

        return RedirectToAction("Index");   
    }
    public async Task<IActionResult>Detail(int id)
    {
        var slider = await _context.Sliders.SingleOrDefaultAsync(s =>s.Id ==id);
        if (slider == null)
            return NotFound();

        return View(slider);

    }
    public async Task<IActionResult>Delete(int id)
    {
        var slider = _context.Sliders.FirstOrDefault(s => s.Id == id);
        if (slider == null) return NotFound();
        return View(slider);
    }
    [HttpPost]
    [ActionName("Delete")]
    public async Task<IActionResult> DeleteSlider(int id)
    {
        var slider = _context.Sliders.FirstOrDefault(s => s.Id ==id);
        if (slider == null) return NotFound();

        _context.Sliders.Remove(slider);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
    public async Task<IActionResult> Update(int id)
    {
        var slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
        if (slider == null) return NotFound();

        return View(slider);
    }
    [HttpPost]
    public async Task<IActionResult>Update(int id, Slider slider)
    {
        var dbSlider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
            if(dbSlider == null) return NotFound();

            dbSlider.Title = slider.Title;
            dbSlider.Offer = slider.Offer;
            dbSlider.Discription = slider.Discription;
            dbSlider.Image = slider.Image;

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));


    }
}
