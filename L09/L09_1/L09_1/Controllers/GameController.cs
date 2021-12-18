using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L09_1.Controllers
{
    public class GameController : Controller
    {
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
            if (maxAsString == null)
            {
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
            if (selectedAsString == null)
            {
                ViewBag.Message = $"Value not selected.";
            }
            else
            {
                int selected = (int)selectedAsString;
                int count = (int)TempData["count"];
                count += 1;
                if (clientGuess < selected)
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


            return View("Zad2");
        }
    }
}
