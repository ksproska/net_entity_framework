using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using L12.Data;
using L12.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace L12.Pages.Articles
{
    public class DeleteModel : PageModel
    {
        private readonly L12.Data.ShopDbContext _context;
        private IHostingEnvironment _hostingEnviroment;

        public DeleteModel(L12.Data.ShopDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnviroment = hostingEnvironment;
        }

        [BindProperty]
        public Article Article { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Article = await _context.Article
                .Include(a => a.Category).FirstOrDefaultAsync(m => m.Id == id);

            if (Article == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Article = await _context.Article.FindAsync(id);

            if (Article != null)
            {
                if (Article.ImageFilename != null && Article.ImageFilename != "")
                {
                    string uploadFolder = Path.Combine(_hostingEnviroment.WebRootPath, "upload");
                    string path = Path.GetFullPath(Path.Combine(uploadFolder, Article.ImageFilename));
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                }
                _context.Article.Remove(Article);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
