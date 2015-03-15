using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NFLQuarterbacks.Controllers
{
    public class HomeController : Controller
    {
        private Manager m = new Manager();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ReadFromCSV()
        {
            return View(m.ReadFromCSV());
        }

        public ActionResult LoadFromCSV()
        {
            return View(m.LoadFromCSV());
        }

        public ActionResult ReadFromXLSX()
        {
            return View(m.ReadFromXLSX());
        }

        public ActionResult RemoveStoredData()
        {
            m.RemoveStoredData();
            return View();
        }
        /*
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
        */

    }

}