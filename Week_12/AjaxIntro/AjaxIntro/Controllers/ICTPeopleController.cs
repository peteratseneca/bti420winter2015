using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// added...
using System.IO;
using CsvHelper;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace AjaxIntro.Controllers
{
    public class ICTPeopleController : Controller
    {
        public List<ICTPerson> Persons { get; set; }

        public ICTPeopleController()
        {
            // Get the file system path to the data
            // Must use the AppDomain class when inside a controller constructor
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data/ictpeople.csv");

            // Create a file system reader
            StreamReader sr = System.IO.File.OpenText(path);

            // Create and use the CSV reader
            var csvReader = new CsvReader(sr);
            this.Persons = csvReader.GetRecords<ICTPerson>().ToList();

            // Clean up
            sr.Close();
            sr = null;
        }

        // GET: ICTPeople
        public ActionResult Index()
        {
            return View(Persons);
        }

        // GET: ICTPeople/v1
        public ActionResult V1()
        {
            return View(Persons);
        }

        /*
        public ActionResult V2()
        {
            var s = new System.Web.Script.Serialization.JavaScriptSerializer();

            var persons = new List<ICTPersonData>();

            int counter = 1;
            foreach (var p in Persons)
            {
                persons.Add(new ICTPersonData { value = counter, text = p.FullName });
                counter++;
            }

            ViewBag.Names = s.Serialize(persons);

            return View();
        }
        */
        

    }

    public class ICTPersonData
    {
        public int value { get; set; }
        public string text { get; set; }
    }

    public class ICTPerson
    {
        // Properties with stored data

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Display(Name = "Given name(s)")]
        public string GivenNames { get; set; }

        [Display(Name = "Office")]
        public string RoomNumber { get; set; }

        [Display(Name = "Email address")]
        public string Email { get; set; }


        // Generated property values

        [Display(Name = "Full name")]
        public string FullName 
        { 
            get
            {
                return string.Format("{0} {1}", GivenNames, LastName);
            }
        }

        [Display(Name = "Photo URL")]
        public string PhotoUrl
        {
            get
            {
                return string.Format("https://scs.senecac.on.ca/system/files/staff-picture/{0}{1}.jpg", GivenNames, LastName);
            }
        }
    }

}