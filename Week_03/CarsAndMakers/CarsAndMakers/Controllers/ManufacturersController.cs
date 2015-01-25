using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarsAndMakers.Controllers
{
    public class ManufacturersController : Controller
    {
        // Create an instance of the Manager class, which will handle the app's data
        private Manager m = new Manager();

        // GET: Manufacturers
        public ActionResult Index()
        {
            // Fetch the manufacturers data
            // The "List" scaffold template was selected, specifying the "ManufacturersBase" model class
            return View(m.AllManufacturers);
        }

        // GET: Manufacturers/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Manufacturers/Create
        public ActionResult Create()
        {
            // Handles HTTP GET, and returns a view with an HTML Form

            return View();
        }

        // POST: Manufacturers/Create
        [HttpPost]
        public ActionResult Create(ManufacturerAdd newItem)
        {
            // Handles HTTP POST, and adds the new item, then redirects to another view
            // Notice that this method is prefixed with an "attribute", [HttpPost]

            ManufacturerBase addedItem = null;

            // Check that the incoming data is valid
            if (ModelState.IsValid)
            {
                addedItem = m.AddManufacturer(newItem);
            }
            else
            {
                // Return the object so the user can edit it correctly
                return View(newItem);
            }

            // If the incoming data is valid and the new data was added, redirect
            return RedirectToAction("index");

            // Another alternative is to redirect to the "details" view, like this...
            //return RedirectToAction("details", new { id = addedItem.Id });
        }

        // GET: Manufacturers/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Manufacturers/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Manufacturers/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Manufacturers/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
