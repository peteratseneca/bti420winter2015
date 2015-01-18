using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntroToForms.Controllers
{
    public class LearnFormsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        // ############################################################

        // From the URI as query string values

        public ActionResult FromUri(string firstName)
        {
            // The request processing framework parses ALL query string
            // key-value pairs, but important ones should be method parameters
            // The values are always strings

            // It's possible that there will be no query string
            // (and therefore no passed-in arguments) 
            // Must test for that to prevent an exception

            // Create a "Dictionary" collection of the query string data
            Dictionary<string, string> queryStringData = new Dictionary<string, string>();

            // Add all the query string data
            foreach (var item in Request.QueryString.AllKeys)
            {
                queryStringData.Add(item, Request.QueryString[item]);
            }

            // If the important data was not passed in...
            if (string.IsNullOrEmpty(firstName))
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

        public ActionResult FromRequestBody()
        {
            string userInput =
                string.IsNullOrEmpty((string)Session["userInput"]) ?
                "(nothing yet)" :
                (string)Session["userInput"];

            ViewBag.UserInput = userInput;

            return View();
        }

        // To be safe, should always prefix the method with an [HttpPost] attribute

        [HttpPost]
        public ActionResult HandleFromRequestBody()
        {
            // Notice that this method has no arguments
            // It uses the Request.Form[] form data collection

            Session["userInput"] = Request.Form["userInput"];

            // After the form post handler completes its work, it ALWAYS
            // redirects to a page/view (same page, or other page)

            return RedirectToAction("FromRequestBody");
        }

        // ############################################################

        // HTML Form, using the preferred way, a strongly-typed view model

        // We ALWAYS use two methods with an HTML Form
        // 1. One method displays the form
        // 2. The other method handles the form's posted data

        public ActionResult ViewModel()
        {
            // Fetch from session state
            List<Person> persons;
            persons = (List<Person>)Session["persons"] == null ?
                new List<Person>() :
                (List<Person>)Session["persons"];

            // Create person collection object
            var personcollection =
                new PersonCollection()
                    {
                        Persons = persons
                          .OrderBy(ln => ln.LastName)
                          .ThenBy(gn => gn.GivenNames)
                          .ToList()
                    };
            // Add a status message
            switch (persons.Count)
            {
                case 0:
                    personcollection.StatusMessage = "(no persons on the list yet)";
                    break;
                case 1:
                    personcollection.StatusMessage = "There is 1 person on the list";
                    break;
                default:
                    personcollection.StatusMessage =
                        string.Format("There are {0} persons on the list", persons.Count);
                    break;
            }

            return View(personcollection);
        }

        // To be safe, should always prefix the method with an [HttpPost] attribute

        [HttpPost]
        public ActionResult HandleViewModel(Person newperson)
        {
            // Notice that this method has a strongly-typed 'Person' parameter
            // That's good, because we can use 'model validation'

            if (ModelState.IsValid)
            {
                // Add the passed-in object to the collection
                List<Person> persons;
                persons = (List<Person>)Session["persons"] == null ?
                    new List<Person>() :
                    (List<Person>)Session["persons"];

                persons.Add(newperson);

                // Save back to session state
                Session["persons"] = persons;
            }

            // After the form post handler completes its work, it ALWAYS
            // redirects to a page/view (same page, or other page)

            return RedirectToAction("ViewModel");
        }

    }

}
