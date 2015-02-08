using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssociationsIntro.Controllers
{
    public class SubjectsController : Controller
    {
        private Manager m = new Manager();

        // GET: Subjects
        public ActionResult Index()
        {
            // Fetch and return a collection to the view

            return View(new List<SubjectBase>());
        }

        // GET: Subjects/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Subjects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Subjects/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Subjects/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Subjects/Edit/5
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

        // GET: Subjects/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Subjects/Delete/5
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
