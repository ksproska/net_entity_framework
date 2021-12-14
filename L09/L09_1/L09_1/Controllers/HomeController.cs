using L09_1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Zad1prev;

namespace L09_1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult Zad1(int iA, int iB, int iC)
        {
            ViewData["equation"] = Zad1prev.Zad1prev.GetEquation(iA, iB, iC);
            (int Type, double? x0, double? x1) resultTuple = Zad1prev.Zad1prev.GetResultsTuple(iA, iB, iC);

            ViewData["nrOfResults"] = resultTuple.Type;
            ViewBag.cls = $"t{resultTuple.Type}";
            if (resultTuple.Type == -1)
            {
                ViewData["nrOfResults"] = "infinity";
                ViewBag.cls = "infinity";
            }

            List<double> results = new List<double>();
            if (resultTuple.Type == 1)
            {
                results.Add((double) resultTuple.x0);
            }
            if (resultTuple.Type == 2)
            {
                results.Add((double)resultTuple.x0);
                results.Add((double) resultTuple.x1);
            }

            ViewData["results"] = results;
            return View();
        }
    }
}
