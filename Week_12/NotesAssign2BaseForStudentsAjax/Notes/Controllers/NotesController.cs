using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Notes.Controllers
{
    [Authorize]
    public class NotesController : Controller
    {
        private Manager m = new Manager();

        // GET: Notes
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            throw new NotImplementedException();
        }

        // GET: Notes/Details/5
        public ActionResult Details(int? id)
        {
            throw new NotImplementedException();

            // Determine whether we can continue, non-null id

            // Attempt to fetch the object

            // Present the view
        }

        // GET: Notes/Create/{id}
        public ActionResult Create()
        {
            throw new NotImplementedException();

            // Validate the user name 

            // If validation fails, exit

            // Otherwise, continue...
            // Create and configure an 'add form'
        }

        // POST: Notes/Create
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(NoteAdd newItem)
        {
            throw new NotImplementedException();

            // Standard 'add new' handling
            // Including checking the ModelState

            // If there's a problem with the form data postback, redisplay the form
            // Otherwise, whether successful or not, redirect back to the employee's
            // details view
        }

        /*
        // GET: Notes/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Notes/Edit/5
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

        // GET: Notes/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Notes/Delete/5
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
