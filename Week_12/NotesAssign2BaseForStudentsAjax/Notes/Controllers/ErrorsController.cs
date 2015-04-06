using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Notes.Controllers
{
    // Inspiration: http://stackoverflow.com/a/9026907 

    public sealed class ErrorsController : Controller
    {
        public ActionResult NotFound()
        {
            ViewBag.StatusCode = Response.StatusCode.ToString();

            return View();
        }

        public ActionResult ServerError()
        {
            ViewBag.StatusCode = Response.StatusCode.ToString();

            // If running on localhost (for you, the programmer), 
            // display the error message
            // We assemble the strings here because remote users 
            // must not see any of this content
            if (Request.IsLocal)
            {
                ViewBag.Prompt = "Error message:";
                ViewBag.Error = HttpContext.Error.Message;
            }

            return View();
        }
    }
}
