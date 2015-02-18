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
        public ActionResult Details(int? id)
        {
            // Notice that we changed the parameter type to nullable

            // Determine whether we can continue
            if (!id.HasValue) { return HttpNotFound(); }

            // Fetch the object, so that we can inspect its value
            var fetchedObject = m.GetAlbumByIdWithSongs(id.Value);

            if (fetchedObject == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(fetchedObject);
            }
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

        // GET: <controller>/Edit/5
        public ActionResult Edit(int? id)
        {
            // Notice that we changed the parameter type to nullable

            // Determine whether we can continue
            if (!id.HasValue) { return HttpNotFound(); }

            // Attempt to fetch the object
            var fetchedObject = m.GetAlbumById(id.Value);

            if (fetchedObject == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Create and configure an 'edit form'
                var editForm = AutoMapper.Mapper.Map<AlbumEditForm>(fetchedObject);

                editForm.Genres = new SelectList(m.AllGenres(), fetchedObject.Genre);

                return View(editForm);
            }
        }

        // POST: <controller/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, AlbumEdit newItem)
        {
            if (ModelState.IsValid & id == newItem.Id)
            {
                // Attempt to do the update
                var editedItem = m.EditAlbum(newItem);

                if (editedItem == null)
                {
                    // There was a problem updating the object
                    var editForm = AutoMapper.Mapper.Map<AlbumEditForm>(newItem);
                    editForm.Genres = new SelectList(m.AllGenres(), newItem.Genre);

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
                var editForm = AutoMapper.Mapper.Map<AlbumEditForm>(newItem);
                editForm.Genres = new SelectList(m.AllGenres(), newItem.Genre);

                ModelState.AddModelError("modelState", "There was an error. (The incoming data is invalid.)");

                return View(editForm);
            }
        }

        // GET: <controller>/Delete/5
        public ActionResult Delete(int? id)
        {
            // Notice that we changed the parameter type to nullable

            // Determine whether we can continue
            if (!id.HasValue) { return HttpNotFound(); }

            // Attempt to fetch the object to be deleted
            var itemToDelete = m.GetAlbumById(id.Value);

            if (itemToDelete == null)
            {
                return HttpNotFound();
                // Alternatively... could return...
                return RedirectToAction("index");
            }
            else
            {
                return View(itemToDelete);
            }
        }

        // POST: <controller>/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, FormCollection collection)
        {
            // Notice that we changed the parameter type to nullable

            // Determine whether we can continue
            if (!id.HasValue) { return HttpNotFound(); }

            // Attempt to delete the item
            m.DeleteAlbumById(id.Value);

            return RedirectToAction("index");
        }

    }
}
