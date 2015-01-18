using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersistDataInMemory.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // Persist data in memory
            // We will use ASP.NET Session State to save/persist data in memory 

            // ############################################################
            // Round-trip save-then-fetch an int value to/from Session State

            // Save
            int myAge = 29;
            Session["myAge"] = myAge;

            // ############################################################
            // Round-trip save-then-fetch a string value to/from Session State

            // Save
            string myName = "Peter";
            Session["myName"] = myName;

            // ############################################################
            // Round-trip save-then-fetch a simple collection to/from Session State

            // Save
            var colours = new List<string>();
            colours.Add("red");
            colours.Add("green");
            colours.Add("blue");
            colours.Add("purple");
            colours.Add("black");
            colours.Add("brown");
            colours.Add("yellow");
            colours.Add("gold");
            Session["colours"] = colours;

            // ############################################################
            // Round-trip save-then-fetch a collection of custom objects to/from Session State

            // Save
            // Create a Person object
            Person peter = new Person() { Id = 2, Name = "Peter" };
            peter.FavouriteColours.Add("blue");
            peter.FavouriteColours.Add("red");
            // Create another person object, and assign the colour collection
            Person elliott = new Person() { Id = 4, Name = "Elliott", FavouriteColours = colours };
            List<Person> teachers = new List<Person>();
            teachers.Add(peter);
            teachers.Add(elliott);
            Session["teachers"] = teachers;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Fetch the values saved in the Index() method";

            // Fetch; test existence first
            int myAge;
            if (Session["myAge"] != null)
            {
                myAge = (int)Session["myAge"];
            }
            else
            {
                myAge = 0;
            }

            // Fetch
            string myName;
            if (Session["myName"] != null)
            {
                myName = (string)Session["myName"];
            }
            else
            {
                myName = "";
            }

            // Fetch
            List<string> colours;
            if (Session["colours"] != null)
            {
                colours = (List<string>)Session["colours"];
            }
            else
            {
                colours = new List<string>();
            }

            // Fetch
            List<Person> teachers;
            if (Session["teachers"] != null)
            {
                teachers = (List<Person>)Session["teachers"];
            }
            else
            {
                teachers = new List<Person>();
            }

            // Prepare data to pass to the view
            AboutViewModel answers = new AboutViewModel();
            answers.MyAge = myAge;
            answers.MyName = myName;
            answers.Colours = colours;
            answers.Teachers = teachers;

            return View(answers);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact info";

            return View();
        }
    }
}