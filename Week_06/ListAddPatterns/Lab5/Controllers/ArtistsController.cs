using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab5.Controllers
{
    public class ArtistsController : Controller
    {
        private Manager m = new Manager();

        // GET: Artists
        public ActionResult Index()
        {
            return View(m.AllArtists());
        }

        // GET: Artists/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Artists/Create
        public ActionResult Create()
        {
            // Create and configure an 'add form'
            var addForm = new ArtistAddForm();

            // Fetch the genres
            addForm.Genre = new SelectList(m.AllGenres());

            // Set a reasonable start date in the user interface
            addForm.BirthOrStartDate = DateTime.Now.AddYears(-30);

            // Set a reasonable start decade in the user interface
            // The formula rounds off a date to the nearest 10
            addForm.StartDecade = ((int)((DateTime.Now.Year - 10) / 10)) * 10;

            return View(addForm);
        }

        // POST: Artists/Create
        [HttpPost]
        public ActionResult Create(ArtistAdd newItem)
        {
            if (ModelState.IsValid)
            {
                // Attempt to add the item
                var addedItem = m.AddArtist(newItem);

                if (addedItem == null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View();
            }
        }

    }
}
