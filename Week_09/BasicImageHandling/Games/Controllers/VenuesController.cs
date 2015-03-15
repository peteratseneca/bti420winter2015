using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Games.Controllers
{
    public class VenuesController : Controller
    {
        private Manager m = new Manager();

        // GET: Venues
        public ActionResult Index()
        {
            return View(m.AllVenues());
        }

        // GET: Venues/Details/5
        public ActionResult Details(int? id)
        {
            // Notice that we changed the parameter type to nullable

            // Determine whether we can continue
            if (!id.HasValue) { return HttpNotFound(); }

            // Fetch the object, so that we can inspect its value
            var fetchedObject = m.GetVenueByIdWithImage(id.Value);

            if (fetchedObject == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(fetchedObject);
            }
        }

        // GET: Venues/Create
        public ActionResult Create()
        {
            // All done adding now...
            //return RedirectToAction("index");

            return View();
        }

        // POST: Venues/Create
        [HttpPost]
        public ActionResult Create(VenueAdd newItem)
        {
            if (ModelState.IsValid)
            {
                /*
                // Handle the uploaded photo
                byte[] photoBytes = new byte[newItem.PhotoUpload.ContentLength];
                newItem.PhotoUpload.InputStream.Read(photoBytes, 0, newItem.PhotoUpload.ContentLength);
                newItem.Photo = photoBytes;
                newItem.PhotoContentType = newItem.PhotoUpload.ContentType;

                // Handle the uploaded map
                byte[] mapBytes = new byte[newItem.MapUpload.ContentLength];
                newItem.MapUpload.InputStream.Read(mapBytes, 0, newItem.MapUpload.ContentLength);
                newItem.Map = mapBytes;
                newItem.MapContentType = newItem.MapUpload.ContentType;
                */

                // Attempt to add the item
                var addedItem = m.AddVenue(newItem);

                if (addedItem == null)
                {
                    return RedirectToAction("index");
                }
                else
                {
                    return RedirectToAction("index");
                }
            }
            else
            {
                return View();
            }
        }

        /*
        // GET: Venues/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Venues/Edit/5
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

        // GET: Venues/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Venues/Delete/5
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
        */
    }
}
