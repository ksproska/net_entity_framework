using L10_2.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using L10_2.ViewModels;

namespace L10_2.Controllers
{
    public class ShopController : Controller
    {
        ShopDbContext _context;
        public ShopController(ShopDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var shopContextAll = _context.Article.Include(a => a.Category);
            return View(await shopContextAll.ToListAsync());
        }

        [Route("Shop/{CategoryId}")]
        public async Task<IActionResult> Filtered(int CategoryId)
        {
            var shopContextFiltered = _context.Article
                .Where<Article>(item => item.CategoryId == CategoryId)
                .Include(a => a.Category);
            return View("Index", await shopContextFiltered.ToListAsync());
        }
    }
}
