using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Notes.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class OUsController : Controller
    {
        private Manager m = new Manager();

        // GET: OUs
        public ActionResult Index()
        {
            return View(m.GetAllOUs());
        }

        /*
        // GET: OUs/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OUs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OUs/Create
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

        // GET: OUs/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OUs/Edit/5
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

        // GET: OUs/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OUs/Delete/5
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
