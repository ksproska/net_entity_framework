using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using L12.Data;
using L12.Models;

namespace L12.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly L12.Data.ShopDbContext _context;

        public IndexModel(L12.Data.ShopDbContext context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; }

        public async Task OnGetAsync()
        {
            Category = await _context.Category.ToListAsync();
        }
    }
}
