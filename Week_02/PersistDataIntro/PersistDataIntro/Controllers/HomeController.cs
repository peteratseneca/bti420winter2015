using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// more...
using System.IO;
using Biggy.Core;
using Biggy.Data.Json;

namespace PersistDataIntro.Controllers
{
    // Open the NuGet Package Manager Console...
    // TOOLS > NuGet Package Manager > Package Manager Console
    // Run this command...
    // install-package biggy.data.json

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<Person> persons = new List<Person>();

            if (Directory.Exists(Server.MapPath("~/App_Data/company")))
            {
                var app_data = Server.MapPath("~/App_Data");

                // Attach to the data store
                var store = new JsonStore<Person>(app_data, "company", "persons");
                persons = new BiggyList<Person>(store).ToList();

                // Alternative...
                /*
                var storedPersons = new BiggyList<Person>(store);
                foreach (var person in storedPersons)
                {
                    persons.Add(new Person 
                    { 
                        Id = person.Id, 
                        Name = person.Name, 
                        FavouriteColours = person.FavouriteColours 
                    });
                }
                */
            }
            return View(persons);
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

        public string CreateDataStore()
        {
            // Create the data store

            if (!Directory.Exists(Server.MapPath("~/App_Data/company")))
            {
                var app_data = Server.MapPath("~/App_Data");

                // Create a new JSON store
                var store = new JsonStore<Person>(app_data, "company", "persons");
                var persons = new BiggyList<Person>(store);

                // Add some initial data
                persons.Add(new Person { Id = 1, Name = "Peter McIntyre", FavouriteColours = new List<string> { "blue", "green" } });
                persons.Add(new Person { Id = 2, Name = "Junlian Xiang", FavouriteColours = new List<string> { "grey", "blue" } });

                // Display the data
                string result = "";
                foreach (var person in persons)
                {
                    result = string.Format("{0}<br />Person ID {1}, name is {2}", result, person.Id, person.Name);
                }
                return result;
            }
            else
            {
                return "Data store exists; use the /home/addpersons URI";
            }
        }

        public string AddPersons()
        {
            var app_data = Server.MapPath("~/App_Data");

            // Attach to the data store
            var store = new JsonStore<Person>(app_data, "company", "persons");
            var persons = new BiggyList<Person>(store);

            // Add more persons

            int newId = persons.Max(id => id.Id) + 1;
            persons.Add(new Person
            {
                Id = newId,
                Name = "Person number " + newId.ToString(),
                FavouriteColours = new List<string> { "black", "white" }
            });

            newId++;
            persons.Add(new Person
            {
                Id = newId,
                Name = "Person number " + newId.ToString(),
                FavouriteColours = new List<string> { "plaid", "transparent" }
            });

            // Display the data

            string result = "";
            foreach (var person in persons)
            {
                result = string.Format("{0}<br />Person ID {1}, name is {2}", result, person.Id, person.Name);
            }
            return result;
        }

    }
}