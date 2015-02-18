using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab5.Controllers
{
    public class SongsController : Controller
    {
        private Manager m = new Manager();

        // GET: Songs
        public ActionResult Index()
        {
            return View(m.AllSongs());
        }

        // GET: Songs/Details/5
        public ActionResult Details(int? id)
        {
            // Notice that we changed the parameter type to nullable

            // Determine whether we can continue
            if (!id.HasValue) { return HttpNotFound(); }

            // Fetch the object, so that we can inspect its value
            var fetchedObject = m.GetSongById(id.Value);

            if (fetchedObject == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(fetchedObject);
            }
        }

        // GET: Songs/Create
        public ActionResult Create()
        {
            // Create and configure an 'add form'
            var addForm = new SongAddForm();

            // Fetch for and configure the album listbox
            //addForm.AlbumId = new SelectList(m.AllAlbumsList(), "Id", "Name");
            addForm.AlbumId = new SelectList(m.AllAlbumsList(), "Id", "AlbumAndArtist");

            // Fetch and configure the genres
            addForm.Genre = new SelectList(m.AllGenres());
            
            return View(addForm);
        }

        // POST: Songs/Create
        [HttpPost]
        public ActionResult Create(SongAdd newItem)
        {
            if (ModelState.IsValid)
            {
                // Attempt to add the item
                var addedItem = m.AddSong(newItem);

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
