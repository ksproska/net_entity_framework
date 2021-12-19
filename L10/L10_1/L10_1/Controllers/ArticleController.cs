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
using System.Windows;

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
            return View();
        }

        // POST: ArticleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArticleViewModel article)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var formfile = article.FormFile;

                    if (formfile != null)
                    {
                        var filename = formfile.FileName;

                        var newName = Guid.NewGuid().ToString() + filename;
                        string uploadFolder = Path.Combine(_hostingEnviroment.WebRootPath, _dataContext.GetPhotosDestinationFile());
                        using (FileStream DestinationStream = System.IO.File.Create(Path.Combine(uploadFolder, newName)))
                        {
                            formfile.CopyTo(DestinationStream);
                            article.FormFile = new FormFile(formfile.OpenReadStream(), 
                                formfile.Length, formfile.Length, formfile.Name, 
                                newName);
                        }
                    }
                    _dataContext.AddArticle(article);
                }
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
                string uploadFolder = Path.Combine(_hostingEnviroment.WebRootPath, _dataContext.GetPhotosDestinationFile());
                _dataContext.RemoveArticle(id, uploadFolder);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
