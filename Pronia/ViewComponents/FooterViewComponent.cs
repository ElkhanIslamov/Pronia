﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.Contexts;

namespace Pronia.ViewComponents
{
    public class FooterViewComponent:ViewComponent
    {
        private readonly ProniaDbContext _context;

        public FooterViewComponent(ProniaDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult>InvokeAsync()
        {
            var settings = await _context.Settings.ToDictionaryAsync(x=>x.Key,x=>x.Value);
            return View(settings);
        }
    }
}
