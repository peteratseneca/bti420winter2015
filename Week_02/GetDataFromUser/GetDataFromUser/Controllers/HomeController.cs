using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GetDataFromUser.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "About this app";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "General contact info";

            return View();
        }

        // ############################################################
        // New code
        // ############################################################

        // GET: home/fromuri
        public ActionResult FromURI(string firstname)
        {
            // Reading query string values from the URI is most often done in HTTP GET requests

            // The request processing framework parses ALL query string key-value pairs, 
            // but important ones should be declared as method parameters
            // The values are always strings

            // It's possible that there will be no query string
            // (and therefore no passed-in arguments) 
            // Must test for that to prevent an exception

            // Create a "Dictionary" collection for the query string data
            Dictionary<string, string> queryStringData = new Dictionary<string, string>();

            // Add all the query string data
            foreach (var item in Request.QueryString.AllKeys)
            {
                queryStringData.Add(item, Request.QueryString[item]);
            }

            // If the important data was not passed in...
            if (string.IsNullOrEmpty(firstname))
            {
                queryStringData.Add("firstname", "(empty)");
            }

            return View(queryStringData);
        }

        // ############################################################

        // HTML Form, using the form data collection in the request body

        // We ALWAYS use two methods with an HTML Form
        // 1. One method displays the form
        // 2. The other method handles the form's posted data

        // GET: home/requestbody
        public ActionResult RequestBody()
        {
            // Handles HTTP GET, and returns a view with an HTML Form

            // If this is shown as a result of a PRG task, 
            // then TempData will have the data that was temporarily saved
            ViewBag.UserInput = TempData["userInput"] as string;

            return View();
        }

        // POST: home/requestbodypost
        [HttpPost]
        public ActionResult RequestBodyPost()
        {
            // Notice that this method has no arguments
            // It uses the Request.Form[] form data collection

            TempData["userInput"] = Request.Form["userInput"];

            // After the form post handler completes its work, it ALWAYS
            // redirects to a page/view (same page, or other page)

            return RedirectToAction("RequestBody");
        }

        // ############################################################

        // GET: home/viewmodel
        public ActionResult ViewModel()
        {
            // If this is shown as a result of a PRG task, 
            // then TempData will have the data that was temporarily saved
            ViewBag.UserInput = TempData["userInput"] as Person;

            return View();
        }

        // POST: home/viewmodel
        [HttpPost]
        public ActionResult ViewModel(Person newItem)
        {
            // Handles HTTP POST, and adds the new person, then redirects to another view
            // Notice that this method is prefixed with an "attribute", [HttpPost]

            // Check that the incoming data is valid
            if (ModelState.IsValid)
            {
                TempData["userInput"] = newItem;
            }

            // If the incoming data is valid and the new data was added, redirect
            return RedirectToAction("viewmodel");
        }

    }

}
