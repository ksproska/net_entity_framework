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
    public class DeleteModel : PageModel
    {
        private readonly L12.Data.ShopDbContext _context;

        public DeleteModel(L12.Data.ShopDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Category Category { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category = await _context.Category.FirstOrDefaultAsync(m => m.Id == id);

            if (Category == null)
            {
                return NotFound();
            }
            var article = await _context.Article.FirstOrDefaultAsync(a => a.CategoryId == id);
            if (article != null)
            {
                return RedirectToPage("./NotDelete", Category);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category = await _context.Category.FindAsync(id);
            var article = await _context.Article.FirstOrDefaultAsync(a => a.CategoryId == id);

            if (Category != null && article == null)
            {
                _context.Category.Remove(Category);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
