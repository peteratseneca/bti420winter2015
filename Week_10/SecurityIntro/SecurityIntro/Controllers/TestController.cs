using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// added...
using System.Security.Claims;

namespace SecurityIntro.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            // Container for user and claims info
            List<string> allClaims = new List<string>();

            // Is this request authenticated?
            allClaims.Add("Authenticated = " + (User.Identity.IsAuthenticated ? "Yes" : "No"));
            if (User.Identity.IsAuthenticated)
            {
                // Cast the generic principal to a claims-carrying identity
                var identity = User.Identity as ClaimsIdentity;
                // Extract only the claims
                var claims = identity.Claims
                    .Select(c => new { Type = c.Type, Value = c.Value })
                    .AsEnumerable();
                foreach (var claim in claims)
                {
                    // Create a readable string
                    allClaims.Add(claim.Type + " = " + claim.Value);
                }
            }

            return View(allClaims);
        }

        [Authorize]
        public ActionResult AccountRoleAny()
        {
            return Content("Any account works correctly");
        }

        [Authorize(Roles = "User")]
        public ActionResult AccountRoleUser()
        {
            return Content("Role 'User' works correctly");
        }

        [Authorize(Roles = "Student")]
        public ActionResult AccountRoleStudent()
        {
            return Content("Role 'Student' works correctly");
        }

        [Authorize(Roles = "Teacher")]
        public ActionResult AccountRoleTeacher()
        {
            return Content("Role 'Teacher' works correctly");
        }

        [Authorize(Roles = "Coordinator")]
        public ActionResult AccountRoleCoordinator()
        {
            return Content("Role 'Coordinator' works correctly");
        }

        [Authorize(Roles = "Teacher,Coordinator")]
        public ActionResult AccountRoleTeacherCoordinator()
        {
            return Content("Role 'Teacher, Coordinator' works correctly");
        }

    }

}
