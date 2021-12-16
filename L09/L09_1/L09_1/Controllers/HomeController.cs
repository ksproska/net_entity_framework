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

        [Route("Tool/Solve/{iA}/{iB}/{iC}")]
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

        public IActionResult Set(int maxVal)
        {
            if(maxVal <= 0)
            {
                ViewBag.Message = $"Value must be greater than 0.";
                ViewBag.Cls = "error";
                var maxAsString = TempData["max"];
            }
            else
            {
                ViewBag.Message = $"New max value: {maxVal}";
                TempData["max"] = maxVal;
            }
            
            return View("Zad2");
        }

        public IActionResult Draw()
        {
            var maxAsString = TempData["max"];
            if(maxAsString == null) {
                ViewBag.Message = "Couldn't draw value. Max value not set.";
                ViewBag.Cls = "error";
            }
            else
            {
                int max = (int)maxAsString;
                int selected = new Random().Next(max);
                TempData["selected"] = selected;
                TempData["count"] = 0;
                ViewBag.Message = "Draw successful.";
            }
            return View("Zad2");
        }

        public IActionResult Guess(int clientGuess)
        {
            var selectedAsString = TempData["selected"];
            if(selectedAsString == null)
            {
                ViewBag.Message = $"Value not selected.";
            }
            else
            {
                int selected = (int) selectedAsString;
                int count = (int)TempData["count"];
                count += 1;
                if(clientGuess < selected)
                {
                    TempData["selected"] = selected;
                    TempData["count"] = count;
                    ViewBag.Message = $"Too little.";
                    ViewBag.Attempt = $"Attempt: {count}";
                    ViewBag.Cls = $"little";
                }
                else if (clientGuess > selected)
                {
                    TempData["selected"] = selected;
                    TempData["count"] = count;
                    ViewBag.Message = $"Too much.";
                    ViewBag.Attempt = $"Attempt: {count}";
                    ViewBag.Cls = $"much";
                }
                else
                {
                    ViewBag.Message = $"Bingo! Value is {selected}";
                    ViewBag.Attempt = $"Attempt: {count}";
                    ViewBag.Cls = $"bingo";
                }
            }
            

            return View();
        }
    }
}
