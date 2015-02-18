using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab5.Controllers
{
    public class AlbumsController : Controller
    {
        private Manager m = new Manager();

        // GET: Albums
        public ActionResult Index()
        {
            return View(m.AllAlbums());
        }

        // GET: Albums/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Albums/Create
        public ActionResult Create()
        {
            // Create and configure an 'add form'
            var addForm = new AlbumAddForm();

            // Fetch for and configure the artist listbox
            addForm.ArtistId = new SelectList(m.AllArtistsList(), "Id", "Name");

            // Fetch and configure the genres
            addForm.Genre = new SelectList(m.AllGenres());

            // Set a reasonable date
            addForm.ReleaseDate = DateTime.Now;

            return View(addForm);
        }

        /*
        
        // This does not work for some reason - wasted half-hour

            // Fetch the artists
            var artists = m.AllArtistsList();

            // Get the 'value' of the first element in the artists collection
            // We want to use it to pre-select an item on the listbox
            var selected = artists.ElementAt(0).Id.ToString();

            // Configure the artist listbox; notice the use of the 'selected' variable
            addForm.ArtistId = new SelectList(artists, "Id", "Name", selected);
        
        */

        // POST: Albums/Create
        [HttpPost]
        public ActionResult Create(AlbumAdd newItem)
        {
            if (ModelState.IsValid)
            {
                // Attempt to add the item
                var addedItem = m.AddAlbum(newItem);

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
