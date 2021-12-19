using L10_1.ViewModels;
using L10_1.ViewModels.DataContext;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace L10_1.Controllers
{
    public class ArticleController : Controller
    {
        private IDataContext _dataContext;
        private IHostingEnvironment _hostingEnviroment;
        public ArticleController(IDataContext dataContext, IHostingEnvironment hostingEnvironment)
        {
            this._dataContext = dataContext;
            this._hostingEnviroment = hostingEnvironment;
        }

        // GET: ArticleController
        public ActionResult Index()
        {
            return View(_dataContext.GetArticles());
        }

        // GET: ArticleController/Details/5
        public ActionResult Details(int id)
        {
            return View(_dataContext.GetArticle(id));
        }

        // GET: ArticleController/Create
        public ActionResult Create()
        {
            //string uploadFolder = Path.Combine(_hostingEnviroment.WebRootPath, "upload");
            return View(new ArticleViewModel());
        }

        // POST: ArticleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArticleViewModel article)
        {
            try
            {
                if (ModelState.IsValid)
                    _dataContext.AddArticle(article);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ArticleController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_dataContext.GetArticle(id));
        }

        // POST: ArticleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ArticleViewModel article)
        {
            try
            {
                article.Id = id;
                _dataContext.UpdateArticle(article);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ArticleController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_dataContext.GetArticle(id));
        }

        // POST: ArticleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _dataContext.RemoveArticle(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
