using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Notes.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private Manager m = new Manager();

        // GET: Employees
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            throw new NotImplementedException();
        }

        // GET: Employees/DetailsByUserName/{username}
        // The purpose of this method is to support a username lookup,
        // but as you can see, it simply redirects with the user identifier
        // to the next method
        public ActionResult DetailsByUserName(string username)
        {
            throw new NotImplementedException();

            // Determine whether we can continue, non-null username

            // Attempt to fetch the object

            // If found, redirect to the Details method, specifying the Employee Id
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            throw new NotImplementedException();

            // Determine whether we can continue, non-null id

            // Attempt to fetch the object

            // Prepare the object for the view
            // Present the view
        }

        // GET: Employees/ChooseDirectReports/5
        // Sends the form to enable a manager to select their direct reports
        public ActionResult ChooseDirectReports(int? id)
        {
            throw new NotImplementedException();

            // Determine whether we can continue, non-null id

            // Verify that the security context user has the 'Manager' role claim
            // e.g. (User as ClaimsPrincipal).HasClaim(ClaimTypes.Role, "Manager")

            // Prepare and configure a form to send to the view
            // Present the view
        }

        // POST: Employees/ChooseDirectReports/5
        // Handles the selection of direct reports
        [HttpPost]
        public ActionResult ChooseDirectReports(int? id, EmployeeDirectReports newItem)
        {
            if (ModelState.IsValid & id == newItem.Id)
            {
                // Attempt to perform the task
                var addedItem = m.ConfigureDirectReports(newItem);

                if (addedItem == null)
                {
                    // There was a problem, just do a redirect
                    return RedirectToAction("Details", new { id = id });
                }
                else
                {
                    // Maybe return to a results page
                    return View("DirectReports", addedItem);
                }
            }
            else
            {
                // There was a problem, just do a redirect
                return RedirectToAction("Details", new { id = id });
            }
        }

    }
}
