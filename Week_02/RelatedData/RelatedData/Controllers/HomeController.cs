using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RelatedData.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // Most of this code is from the console app example

            // Create a Supplier object
            Supplier microsoft = new Supplier()
            {
                Id = 1,
                Name = "Microsoft Corporation",
                Country = "USA"
            };

            // Create a Product object
            var keyboard = new Product()
            {
                Id = 2,
                Name = "Keyboard",
                MSRP = 45,
                ProductCode = "qwe123",
                UPC = "0987654321"
            };

            // Add the Product object to the Supplier's collection of Product objects
            microsoft.Products.Add(keyboard);
            // For in-memory only objects, we must also set the other end
            keyboard.Supplier = microsoft;

            // Add a new Product object directly to the Supplier's collection
            // Notice that we set the supplier property for this in-memory only object
            microsoft.Products.Add(new Product()
            {
                Id = 1,
                MSRP = 25,
                Name = "Mouse",
                ProductCode = "abc123",
                UPC = "1234567890",
                Supplier = microsoft
            });

            // Pass the Supplier object to the view

            return View(microsoft);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Data relations/associations";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact info";

            return View();
        }
    }
}