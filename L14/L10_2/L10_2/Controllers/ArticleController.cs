using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using L10_2.Data;
using L10_2.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace L10_2.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ArticleController : Controller
    {
        private readonly ShopDbContext _context;
        private IHostingEnvironment _hostingEnviroment;
        private readonly ILogger<ArticleController> _logger;

        public ArticleController(ShopDbContext context, IHostingEnvironment hostingEnvironment, ILogger<ArticleController> logger)
        {
            _context = context;
            _hostingEnviroment = hostingEnvironment;
            _logger = logger;
        }

        // GET: Article
        public async Task<IActionResult> Index()
        {
            ViewData["maxLen"] = _context.Article.ToList().Count;
            var shopDbContext = _context.Article.Include(a => a.Category).OrderBy(item => item.Id);
            return View(await shopDbContext.ToListAsync());
        }

        // GET: Article/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Article
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // GET: Article/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
            return View();
        }

        // POST: Article/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,CategoryId,FormFile")] Article article)
        {
            if (ModelState.IsValid)
            {
                var formfile = article.FormFile;
                if (formfile != null)
                {
                    var filename = formfile.FileName;
                    var newName = Guid.NewGuid().ToString() + filename;
                    string uploadFolder = Path.Combine(_hostingEnviroment.WebRootPath, "upload");
                    using (FileStream DestinationStream = System.IO.File.Create(Path.Combine(uploadFolder, newName)))
                    {
                        formfile.CopyTo(DestinationStream);
                        article.ImageFilename = newName;
                    }
                }
                _context.Add(article);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", article.CategoryId);
            return View(article);
        }

        // GET: Article/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Article.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", article.CategoryId);
            return View(article);
        }

        // POST: Article/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,CategoryId,ImageFilename")] Article article)
        {
            if (id != article.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // todo
                    var local = await _context.Article.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                    article.ImageFilename = local.ImageFilename;
                    _context.Update(article);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleExists(article.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", article.CategoryId);
            return View(article);
        }

        // GET: Article/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Article
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // POST: Article/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var article = await _context.Article.FindAsync(id);
            if (article.ImageFilename != "" && article.ImageFilename != null)
            {
                string uploadFolder = Path.Combine(_hostingEnviroment.WebRootPath, "upload");
                string path = Path.GetFullPath(Path.Combine(uploadFolder, article.ImageFilename));
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }

            _context.Article.Remove(article);
            Response.Cookies.Delete(article.Id.ToString());
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticleExists(int id)
        {
            return _context.Article.Any(e => e.Id == id);
        }
    }
}
