using L10_1.ViewModels.DataContext;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using L10_1.ViewModels;
using L10_1.ViewModels.DataContext;

namespace L10_1.Controllers
{
    public class ShopController : Controller
    {
        IDataContext _dataContext;
        public ShopController(IDataContext dataContext) 
        {
            _dataContext = dataContext;
        }

        public IActionResult Index(int? inx)
        {
            if (inx == null)
            {
                return View(_dataContext.GetArticles());
            }
            return View(_dataContext.GetArticles((int)inx));
        }

        [Route("Shop/{inx}")]
        [Route("Shop/Index/{inx}")]
        [Route("Shop/Filtered/{inx}")]
        //[Route("Shop/Filtered?id={inx}")]
        public IActionResult Filtered(int? inx)
        {
            if (inx == null)
            {
                return View(_dataContext.GetArticles());
            }
            return View(_dataContext.GetArticles((int)inx));
        }

        //public IActionResult Filtered(int inx)
        //{
        //    return View(_dataContext.GetArticles(inx));
        //}
    }
}
