using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using L12.Data;
using L12.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace L12.Pages.Shop
{
    public class IndexModel : PageModel
    {
        private readonly L12.Data.ShopDbContext _context;

        public IndexModel(L12.Data.ShopDbContext context)
        {
            _context = context;
        }
        
        public IList<Article> Article { get;set; }
        
        public List<Category> Categories { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? CategoryId { get; set; }
        public async Task OnGetAsync()
        {
            Categories = await _context.Category.ToListAsync();
            if (CategoryId == null || CategoryId == -1)
            {
                Article = await _context.Article
                    .Include(a => a.Category).ToListAsync();
            }
            else
            {
                Article = await _context.Article
                    .Where<Article>(item => item.CategoryId == CategoryId)
                    .Include(a => a.Category).ToListAsync();
            }
        }
    }
}
