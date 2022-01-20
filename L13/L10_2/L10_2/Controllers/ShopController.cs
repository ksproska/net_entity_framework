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
using Microsoft.AspNetCore.Authorization;

namespace L10_2.Controllers
{
    [Authorize(Policy = "ExcludeRoles")]
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
            ViewData["CategoryList"] = new SelectList(_context.Category, "Id", "Name", CategoryId);
            //ViewData["SelectedId"] = CategoryId;
            return View("Index", await shopContextFiltered.ToListAsync());
        }

        public async Task<IActionResult> AddCartInShop(int? id)
        {
            string sCount = Request.Cookies[id.ToString()];
            int iCount = 0;
            if (sCount != null)
            {
                iCount = int.Parse(sCount);
            }
            iCount += 1;
            //ViewData["ExtraMessage"] = "Product added to cart";
            Response.Cookies.Append(id.ToString(), iCount.ToString());
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AddCart(int? id)
        {
            string sCount = Request.Cookies[id.ToString()];
            int iCount = 0;
            if (sCount != null)
            {
                iCount = int.Parse(sCount);
            }
            iCount += 1;

            Response.Cookies.Append(id.ToString(), iCount.ToString());
            return RedirectToAction("ShoppingCart");
        }

        public async Task<IActionResult> SubCart(int? id)
        {
            string sCount = Request.Cookies[id.ToString()];
            int iCount = 1;
            iCount = int.Parse(sCount);
            iCount -= 1;
            
            if(iCount > 0)
            {
                Response.Cookies.Append(id.ToString(), iCount.ToString());
            }
            else
            {
                Response.Cookies.Delete(id.ToString());
            }
            return RedirectToAction("ShoppingCart");
        }

        public async Task<IActionResult> DelCart(int? id)
        {
            Response.Cookies.Delete(id.ToString());
            return RedirectToAction("ShoppingCart");
        }

        [Route("Shop/ShoppingCart")]
        public async Task<IActionResult> ShoppingCart()
        {
            var allCartIds = Request.Cookies.Select(item => item.Key).ToList();

            var allCartArticles = _context.Article.Include(a => a.Category)
                .Where<Article>(item => allCartIds.Contains(item.Id.ToString()))
                .Select(item => new CartArticle(item, Request.Cookies[item.Id.ToString()]));

            if(allCartArticles.ToList().Count == 0)
            {
                return View("ShoppingCartEmpty");
            }
            return View(await allCartArticles.ToListAsync());
        }

        [Authorize]
        [Route("Shop/OrderSummary")]
        public async Task<IActionResult> OrderSummary()
        {
            var allCartIds = Request.Cookies.Select(item => item.Key).ToList();

            var allCartArticles = _context.Article.Include(a => a.Category)
                .Where<Article>(item => allCartIds.Contains(item.Id.ToString()))
                .Select(item => new CartArticle(item, Request.Cookies[item.Id.ToString()]));

            if (allCartArticles.ToList().Count == 0)
            {
                return View("ShoppingCartEmpty");
            }
            var orderDetails = new OrderDetails() {
                CartArticles = allCartArticles.ToList(),
                //ArticleIdWithRepetition = new List<int>()
            };

            ViewData["PaymentOptionId"] = new SelectList(_context.PaymentOption, "Id", "Name");

            return View(orderDetails);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Shop/OrderFinished")]
        public async Task<IActionResult> OrderFinished([Bind("Id,Name,Surname,PhoneNumber,City,Postcode,PaymentOptionId")] OrderDetails orderDetails)
        {
            orderDetails.PaymentOption = await _context.PaymentOption
                .FirstOrDefaultAsync(m => m.Id == orderDetails.PaymentOptionId);

            var allCartIds = Request.Cookies.Select(item => item.Key).ToList();
            var allCartArticles = _context.Article.Include(a => a.Category)
                .Where<Article>(item => allCartIds.Contains(item.Id.ToString()))
                .Select(item => new CartArticle(item, Request.Cookies[item.Id.ToString()]));

            foreach (var x in allCartArticles)
            {
                Response.Cookies.Delete(x.Id.ToString());
            }

            return View(orderDetails);
        }
    }
}
