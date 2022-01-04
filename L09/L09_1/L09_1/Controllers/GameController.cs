using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L09_1.Controllers
{
    public class GameController : Controller
    {
        private readonly ILogger<GameController> _logger;

        public GameController(ILogger<GameController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Set(int maxVal)
        {
            if (maxVal <= 0)
            {
                ViewBag.Message = $"Value must be greater than 0.";
                ViewBag.Cls = "error";
            }
            else
            {
                HttpContext.Session.SetInt32("max", maxVal);
                ViewBag.Message = $"New max value: {maxVal}";
            }

            return View("Zad2");
        }

        public IActionResult Draw()
        {
            int? max = HttpContext.Session.GetInt32("max");
            if (max == null)
            {
                ViewBag.Message = "Couldn't draw value. Max value not set.";
                ViewBag.Cls = "error";
            }
            else
            {
                int selected = new Random().Next((int)max);
                HttpContext.Session.SetInt32("selected", selected);
                HttpContext.Session.SetInt32("count", 0);
                ViewBag.Message = "Draw successful.";
            }
            return View("Zad2");
        }

        public IActionResult Guess(int clientGuess)
        {
            int? selected = HttpContext.Session.GetInt32("selected");
            if (selected == null)
            {
                ViewBag.Message = $"Value not selected.";
            }
            else
            {
                int count = (int)HttpContext.Session.GetInt32("count"); ;
                count += 1;
                if (clientGuess < selected)
                {
                    HttpContext.Session.SetInt32("selected", (int)selected);
                    HttpContext.Session.SetInt32("count", count);
                    ViewBag.Message = $"Too little.";
                    ViewBag.Attempt = $"Attempt: {count}";
                    ViewBag.Cls = $"little";
                }
                else if (clientGuess > selected)
                {
                    HttpContext.Session.SetInt32("selected", (int)selected);
                    HttpContext.Session.SetInt32("count", count);
                    ViewBag.Message = $"Too much.";
                    ViewBag.Attempt = $"Attempt: {count}";
                    ViewBag.Cls = $"much";
                }
                else
                {
                    ViewBag.Message = $"Bingo! Value is {selected}";
                    ViewBag.Attempt = $"Attempt: {count}";
                    ViewBag.Cls = $"bingo";
                    HttpContext.Session.Clear();
                }
            }


            return View("Zad2");
        }
    }
}
