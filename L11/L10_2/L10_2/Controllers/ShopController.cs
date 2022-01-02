using L10_2.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using L10_2.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace L10_2.Controllers
{
    public class ShopController : Controller
    {
        ShopDbContext _context;
        private readonly ILogger<ShopController> _logger;
        public ShopController(ShopDbContext context, ILogger<ShopController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var shopContextAll = _context.Article.Include(a => a.Category);
            ViewData["CategoryList"] = new SelectList(_context.Category, "Id", "Name");
            //ViewData["SelectedId"] = -1;
            return View(await shopContextAll.ToListAsync());
        }

        [Route("Shop/{CategoryId}")]
        public async Task<IActionResult> Filtered(int CategoryId)
        {
            var shopContextFiltered = _context.Article
                .Where<Article>(item => item.CategoryId == CategoryId)
                .Include(a => a.Category);
            ViewData["CategoryList"] = new SelectList(_context.Category, "Id", "Name");
            //ViewData["SelectedId"] = CategoryId;
            return View("Index", await shopContextFiltered.ToListAsync());
        }

        public async Task<IActionResult> AddToCart(int? id)
        {
            string sCount = Request.Cookies[id.ToString()];
            int iCount = 1;
            iCount = int.Parse(sCount);
            iCount += 1;

            Response.Cookies.Append(id.ToString(), iCount.ToString());

            var allCartIds = Request.Cookies.Select(item => item.Key).ToList();
            //allCartIds.Add(id.ToString());
            var allCartArticles = _context.Article
                .Where<Article>(item => allCartIds.Contains(item.Id.ToString()))
                .Select(item => new CartArticle(item, Request.Cookies[item.Id.ToString()]));

            return View("ShoppingCart", await allCartArticles.ToListAsync());
        }

        [Route("Shop/ShoppingCart")]
        public async Task<IActionResult> ShoppingCart()
        {
            var allCartIds = Request.Cookies.Select(item => item.Key).ToList();
            var allCartArticles = _context.Article
                .Where<Article>(item => allCartIds.Contains(item.Id.ToString()))
                .Select(item => new CartArticle(item, Request.Cookies[item.Id.ToString()]));

            return View(await allCartArticles.ToListAsync());
        }
    }
}
