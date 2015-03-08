using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RichTextEditor.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        // ########## Like any editing task, we need two methods

        public ActionResult EditContent()
        {
            // In this example, we are using TempData for data persistence
            // Why? It's quick-and-easy, and enables you to focus on 
            // the rich text editing task

            // Fetch from TempData
            string content = (string)TempData["content"];
            // Save in ViewBag
            ViewBag.Content = content;

            return View();
        }

        // Note the use of ValidateInput, which enables posting HTML content 
        [HttpPost, ValidateInput(false)]
        public ActionResult EditContent(FormCollection collection)
        {
            // In YOUR app, you would be extracting the content
            // from a view model object, and saving it somewhere
            // via a Manager class method

            // However, in this app, we'll just save the content into TempData
            TempData["content"] = collection["editor1"];

            return RedirectToAction("EditContent");
        }

        // ############################################################

    }
}