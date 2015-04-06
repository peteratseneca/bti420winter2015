using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AjaxIntro.Controllers
{
    public class HomeController : Controller
    {
        public List<string> Colours { get; set; }

        public HomeController()
        {
            this.Colours = new List<string> { "Red", "Green", "Blue", "Brown", "Yellow", "Black" };
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult DailyDeal()
        {
            ViewBag.Content = "Hello, world!";

            return PartialView("_DailyDeal");
        }

        public ActionResult ColourSearch(string q)
        {
            var results = Colours.Where(c => c.Contains(q));
            return PartialView("_SearchResults", results);
        }

    }
}