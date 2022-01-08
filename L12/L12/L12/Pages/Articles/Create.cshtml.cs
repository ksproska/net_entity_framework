using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using L12.Data;
using System.IO;
using L12.Models;
using Microsoft.AspNetCore.Hosting;

namespace L12.Pages.Articles
{
    public class CreateModel : PageModel
    {
        private readonly L12.Data.ShopDbContext _context;
        private IHostingEnvironment _hostingEnviroment;

        public CreateModel(L12.Data.ShopDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnviroment = hostingEnvironment;
        }

        public IActionResult OnGet()
        {
        ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Article Article { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var formfile = Article.FormFile;
            if (formfile != null)
            {
                var filename = formfile.FileName;
                var newName = Guid.NewGuid().ToString() + filename;
                string uploadFolder = Path.Combine(_hostingEnviroment.WebRootPath, "upload");
                using (FileStream DestinationStream = System.IO.File.Create(Path.Combine(uploadFolder, newName)))
                {
                    formfile.CopyTo(DestinationStream);
                    Article.ImageFilename = newName;
                }
            }

            _context.Article.Add(Article);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
