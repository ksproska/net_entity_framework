using L10_1.ViewModels;
using L10_1.ViewModels.DataContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L10_1.Controllers
{
    public class CategoryController : Controller
    {
        private IDataContext _dataContext;
        public CategoryController(IDataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        // GET: CategoryController
        public ActionResult Index()
        {
            return View(_dataContext.GetCategories());
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(string name)
        {
            return View(_dataContext.GetCategory(name));
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryViewModel category)
        {
            try
            {
                if (ModelState.IsValid)
                    _dataContext.AddCategory(category);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(string name)
        {
            return View(_dataContext.GetCategory(name));
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string oldName, CategoryViewModel category)
        {
            try
            {
                if (ModelState.IsValid)
                    _dataContext.UpdateCategory(oldName, category);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Delete/5
        public ActionResult Delete(string name)
        {
            return View(_dataContext.GetCategory(name));
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(CategoryViewModel category)
        {
            try
            {
                if (ModelState.IsValid)
                    _dataContext.RemoveCategory(category);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
