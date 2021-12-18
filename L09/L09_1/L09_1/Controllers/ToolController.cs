using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L09_1.Controllers
{
    public class ToolController : Controller
    {
        public IActionResult Index()
        {
            return View();
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
                ViewBag.v0 = resultTuple.x0;
            }
            if (resultTuple.Type == 2)
            {
                ViewBag.v0 = resultTuple.x0;
                ViewBag.v1 = resultTuple.x1;
            }

            ViewData["results"] = results;
            return View();
        }
    }
}
