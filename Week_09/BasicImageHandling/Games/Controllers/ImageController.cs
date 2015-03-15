using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Games.Controllers
{
    public class ImageController : Controller
    {
        private Manager m = new Manager();

        [Route("image/sport/logo/{id}")]
        public ActionResult GetSportLogoById(int? id)
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
                // Return a file content result
                // Set the Content-Type header, and return the photo bytes
                return File(fetchedObject.Logo, fetchedObject.LogoContentType);
            }
        }

        [Route("image/sport/photo/{id}")]
        public ActionResult GetSportPhotoById(int? id)
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
                // Return a file content result
                // Seet the Content-Type header, and return the photo bytes
                return File(fetchedObject.Photo, fetchedObject.PhotoContentType);
            }
        }

        [Route("image/venue/photo/{id}")]
        public ActionResult GetVenuePhotoById(int? id)
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
                // Return a file content result
                // Seet the Content-Type header, and return the photo bytes
                return File(fetchedObject.Photo, fetchedObject.PhotoContentType);
            }
        }

        [Route("image/venue/map/{id}")]
        public ActionResult GetVenueMapById(int? id)
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
                // Return a file content result
                // Set the Content-Type header, and return the photo bytes
                // Create a suitable name for the file too
                var fileName = fetchedObject.Name.ToLower().Replace(" ", "-") + GetDefaultFileExtension(fetchedObject.MapContentType);
                return File(fetchedObject.Map, fetchedObject.MapContentType, fileName);
            }
        }

        // Content type to extension is NOT built into the .NET Framework (it appears)
        // Source or inspiration was here...
        // http://stackoverflow.com/questions/23087808/c-sharp-get-file-extension-by-content-type
        private string GetDefaultFileExtension(string contentType)
        {
            string result;
            Microsoft.Win32.RegistryKey key;
            object value;

            key = Microsoft.Win32.Registry.ClassesRoot
                .OpenSubKey(@"MIME\Database\Content Type\" + contentType, false);
            value = key != null ? key.GetValue("Extension", null) : null;
            result = value != null ? value.ToString() : string.Empty;

            return result;
        }

        /*
        // GET: Image
        public ActionResult Index()
        {
            return View();
        }

        // GET: Image/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Image/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Image/Create
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

        // GET: Image/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Image/Edit/5
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

        // GET: Image/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Image/Delete/5
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
