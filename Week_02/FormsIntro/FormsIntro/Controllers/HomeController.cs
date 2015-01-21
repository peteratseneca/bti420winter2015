using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FormsIntro.Controllers
{
    public class HomeController : Controller
    {
        // Create an instance of the Manager class, which will handle the app's data
        private Manager m = new Manager();

        public ActionResult Index()
        {
            // Fetch the persons data
            // Also, replace the template-provided view with a new view
            // Choose the "List" template, specify the "PersonFull" model class
            return View(m.AllPersons);
        }

        public ActionResult About()
        {
            ViewBag.Message = "About this app";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "General contact information";

            return View();
        }

        // ############################################################
        // New code
        // ############################################################

        // Working with a form requires two methods:
        // The first method handles HTTP GET, and returns a view with an HTML Form
        // The other method handles HTTP POST, and adds the data, and redirects to another view
        // This process implements the "PRG" pattern - Post, Redirect, Get

        // By convention, if you want to use scaffolding, the 'add new' method is named Create()

        public ActionResult Create()
        {
            // Handles HTTP GET, and returns a view with an HTML Form

            return View();
        }

        [HttpPost]
        public ActionResult Create(PersonAdd newItem)
        {
            // Handles HTTP POST, and adds the new person, then redirects to another view
            // Notice that this method is prefixed with an "attribute", [HttpPost]

            // Check that the incoming data is valid
            if (ModelState.IsValid)
            {
                var addedItem = m.AddPerson(newItem);
            }
            else
            {
                // Return the object so the user can edit it correctly
                return View(newItem);
            }

            // If the incoming data is valid and the new data was added, redirect
            return RedirectToAction("index");
        }

    }

}