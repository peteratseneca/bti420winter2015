using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AjaxIntro.Controllers
{
    public class WorldController : Controller
    {
        private Manager m = new Manager();

        // GET: World
        public ActionResult Index()
        {
            return View();
        }

        // GET: World/Countries
        public ActionResult Countries()
        {
            return View(m.GetCountries());
        }

        // GET: World/Regions/5
        public ActionResult Regions(int? id)
        {
            if (id == null) { return HttpNotFound(); }

            var fetchedObjects = m.GetRegionsByCountryId(id.Value);

            if (fetchedObjects == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(fetchedObjects);
            }
        }

        // ############################################################
        // The following 'UseInfo' methods (one for GET, one for POST)
        // handle a form that is partially built with Ajax

        // GET: World/UseInfo
        // Create and present an HTML Form that has the Countries collection 
        public ActionResult UseInfo()
        {
            var form = new InfoForm();
            form.Countries = new SelectList(m.GetCountries(), "Id", "Name");

            return View(form);
        }

        // GET: World/FetchRegions/5
        // This is the Ajax listener method
        // It returns a PARTIAL VIEW with the Regions collection for the selected Country
        public ActionResult FetchRegions(int id)
        {
            var form = new InfoFormRegions();
            var regions = m.GetRegionsByCountryId(id);
            // Select the first item in the collection
            form.Regions = new SelectList(regions, "Id", "Name", regions.First().Id);

            return PartialView("_RegionList", form);
        }

        // POST: World/UseInfo
        // Handle the HTML Form data that was submitted
        [HttpPost]
        public ActionResult UseInfo(InfoResult newItem)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Country = m.GetCountryById(newItem.Country);
                ViewBag.Region = m.GetRegionById(newItem.Region);

                return View("UseInfoResult");
            }
            else
            {
                // Something happned, so re-display the form
                return RedirectToAction("UseInfo");
            }
        }

        // ############################################################

    }
}
