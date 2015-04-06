using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// added...
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Notes.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private Manager m = new Manager();

        // GET: Admin
        public ActionResult Index()
        {
            return View(m.GetAllUsers());
        }

        // GET: Admin/Employees
        public ActionResult Employees()
        {
            return View(m.GetAllEmployees());
        }

        // GET: Admin/EmployeeFinder
        public ActionResult EmployeeFinder()
        {
            return View();
        }

        // This is the Ajax listener method
        public ActionResult FindEmployee(string findString)
        {
            var fetchedObjects = m.FindEmployees(findString);

            return PartialView("_FindEmployee", fetchedObjects);
        }

        // GET: Admin/NonManagers
        public ActionResult NonManagers()
        {
            return View(m.GetAllNonManagerUsers());
        }

        // GET: Admin/Configure/{username}/AsManager
        // Remember to configure attribute based routing in App_Start > RouteConfig.cs
        [Route("admin/configure/{username}/asmanager")]
        public ActionResult ConfigureUserAsManager(string username)
        {
            // The username value could be null, so test for this
            if (string.IsNullOrEmpty(username)) { return HttpNotFound(); }

            // It's possible that the user could alredy be a manager
            if (m.IsUserAManager(username)) { return RedirectToAction("nonmanagers"); }

            // If we're here, attempt to fetch the user
            var user = m.GetUserByUserName(username);

            if (user == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Create an edit form
                var editForm = new ApplicationUserEditForm();

                // Set the properties
                editForm.Id = user.Id;
                editForm.FullName = string.Format("{0}, {1}", user.LastName, user.FirstName);
                editForm.UserName = user.UserName;

                // Show the form
                return View(editForm);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("admin/configure/{username}/asmanager")]
        public ActionResult ConfigureUserAsManager(string username, ApplicationUserEdit newItem)
        {
            if (ModelState.IsValid & username == newItem.UserName)
            {
                if (m.IsUserAManager(newItem.UserName))
                {
                    // So, return to the list of non-managers
                    return RedirectToAction("nonmanagers");
                }
                else
                {
                    // Configure as a manager
                    m.ConfigureUserAsManager(newItem.UserName);

                    return RedirectToAction("nonmanagers");
                }
            }
            else
            {
                // There was a problem with model validation or identifiers that don't match
                // Simply re-display the form

                return RedirectToAction("nonmanagers", new { username = newItem.UserName });
            }
        }








        // ########################################
        // Not used, replaced by Manager class methods, called during store initializer seed

        /*
        // POST: DeleteData
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteData()
        {
            // Delete notes
            m.DeleteAllNotes();
            // Delete employees
            m.DeleteAllEmployees();
            // Delete organizational units
            m.DeleteAllOUs();

            TempData["Message"] = "Employee, Note, and OU data has been deleted";
            return RedirectToAction("Index");
        }
        */
    }

}
