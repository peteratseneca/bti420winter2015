using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Games.Controllers
{
    public class SportsController : Controller
    {
        private Manager m = new Manager();

        // GET: Sports
        public ActionResult Index()
        {
            return View(m.AllSports());
        }

        // GET: Sports/Details/5
        public ActionResult Details(int? id)
        {
            // Notice that we changed the parameter type to nullable

            // Determine whether we can continue
            if (!id.HasValue) { return HttpNotFound(); }

            // Fetch the object, so that we can inspect its value
            var fetchedObject = m.GetSportByIdWithImage(id.Value);

            if (fetchedObject == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(fetchedObject);
            }
        }

        // GET: Sports/Create
        public ActionResult Create()
        {
            // All done creating now...
            //return RedirectToAction("index");

            return View();
        }

        // POST: Sports/Create
        [HttpPost]
        public ActionResult Create(SportAdd newItem)
        {
            if (ModelState.IsValid)
            {
                /*
                // Handle the uploaded logo
                byte[] logoBytes = new byte[newItem.LogoUpload.ContentLength];
                newItem.LogoUpload.InputStream.Read(logoBytes, 0, newItem.LogoUpload.ContentLength);
                newItem.Logo = logoBytes;
                newItem.LogoContentType = newItem.LogoUpload.ContentType;

                // Handle the uploaded photo
                byte[] photoBytes = new byte[newItem.PhotoUpload.ContentLength];
                newItem.PhotoUpload.InputStream.Read(photoBytes, 0, newItem.PhotoUpload.ContentLength);
                newItem.Photo = photoBytes;
                newItem.PhotoContentType = newItem.PhotoUpload.ContentType;
                */

                // Attempt to add the item
                var addedItem = m.AddSport(newItem);

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

        // GET: Sports/Edit/5
        public ActionResult Edit(int? id)
        {
            // All done editing now...
            return RedirectToAction("index");

            // Notice that we changed the parameter type to nullable

            // Determine whether we can continue
            if (!id.HasValue) { return HttpNotFound(); }

            // Attempt to fetch the object
            var fetchedObject = m.GetSportByIdWithVenues(id.Value);

            if (fetchedObject == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Create and configure an 'edit form'
                var editForm = AutoMapper.Mapper.Map<SportEditForm>(fetchedObject);

                // Venues
                editForm.Venues = new MultiSelectList(m.AllVenuesForList(), "Id", "Name",
                    fetchedObject.Venues.Select(value => value.Id));

                return View(editForm);
            }
        }

        // POST: Sports/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, SportEdit newItem)
        {
            if (ModelState.IsValid & id == newItem.Id)
            {
                // Attempt to do the update
                var editedItem = m.EditSport(newItem);

                if (editedItem == null)
                {
                    // There was a problem updating the object
                    var editForm = AutoMapper.Mapper.Map<SportEditForm>(newItem);

                    ModelState.AddModelError("modelState", "There was an error. (The edited data could not be saved.)");
                    return View(editForm);
                }
                else
                {
                    // Redirect
                    // Could do other things here
                    return RedirectToAction("details", new { id = editedItem.Id });
                }
            }
            else
            {
                // There was a problem with model validation or identifiers that don't match
                var editForm = AutoMapper.Mapper.Map<SportEditForm>(newItem);

                ModelState.AddModelError("modelState", "There was an error. (The incoming data is invalid.)");

                return View(editForm);
            }
        }

        /*
        // GET: Sports/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Sports/Delete/5
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
