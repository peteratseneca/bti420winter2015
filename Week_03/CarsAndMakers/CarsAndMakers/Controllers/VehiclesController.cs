using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarsAndMakers.Controllers
{
    public class VehiclesController : Controller
    {
        // Create an instance of the Manager class, which will handle the app's data
        private Manager m = new Manager();

        // GET: Vehicles
        public ActionResult Index()
        {
            // Fetch the vehicles data
            // The "List" scaffold template was selected, specifying the "VehicleBase" model class
            //return View(m.AllVehicles);

            // Alternative... fetch the vehicles data, include the manufacturer
            // A new view was created (it replaced the old one), 
            // and hand-edited to add the manufacturer name column
            return View(m.AllVehiclesWithManufacturer());
        }

        // GET: Vehicles/Details/5
        public ActionResult Details(int? id)
        {
            // There are two ways to handle an empty identifier...
            // You see one way here, implemented
            // The other way is to use this as an argument: "int id = 0"
            // and then make the if-test "id == 0"
            // In that situation, you don't need to dereference "id.GetValueOrDefault()"

            // Handle an empty identifier
            if (id == null) { return RedirectToAction("index"); }

            // Attempt to fetch the object
            var fetchedObject = m.GetVehicleById(id.GetValueOrDefault());

            if (fetchedObject == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(fetchedObject);
            }
        }

        // GET: Vehicles/Create
        public ActionResult Create()
        {
            // Handles HTTP GET, and returns a view with an HTML Form
            // Notice that we're initializing an object to push initial data into the view

            // Prepare the data for the view
            var addForm = new VehicleAddForm();
            addForm.ModelYear = DateTime.Now.Year;

            // Configure the "select" user interface control items
            addForm.Manufacturers = new SelectList(m.ListOfManufacturers, "Id", "Name");

            return View(addForm);
        }

        // POST: Vehicles/Create
        [HttpPost]
        public ActionResult Create(VehicleAdd newItem)
        {
            // Handles HTTP POST, and adds the new item, then redirects to another view
            // Notice that this method is prefixed with an "attribute", [HttpPost]

            VehicleBase addedItem = null;

            // Check that the incoming data is valid
            if (ModelState.IsValid)
            {
                addedItem = m.AddVehicle(newItem);
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

        // GET: Vehicles/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Vehicles/Edit/5
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

        // GET: Vehicles/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Vehicles/Delete/5
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
