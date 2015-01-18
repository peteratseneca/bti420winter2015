using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Collections.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Get started with collections";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Render a collection in a view";

            // Create a Person object
            Person peter = new Person() { Id = 2, Name = "Peter" };

            // Now add some objects to the collection...
            peter.FavouriteColours.Add("blue");
            peter.FavouriteColours.Add("red");
            peter.FavouriteColours.Add("green");

            // Pass the object to the view
            return View(peter);
        }
    }
}