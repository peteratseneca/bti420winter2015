using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Family.Controllers
{
    public class PersonsController : Controller
    {
        private Manager m = new Manager();

        // GET: Persons
        public ActionResult Index()
        {
            return View(m.AllPersons());
        }

        // GET: Persons/Details/5
        public ActionResult Details(int id)
        {
            return View(m.PersonById(id));
        }

        // GET: Persons/FamilyEdit/5
        public ActionResult FamilyEdit(int? id)
        {
            // Determine whether we can continue
            if (!id.HasValue) { return HttpNotFound(); }

            // Attempt to fetch the object
            var person = m.PersonByIdWithRelations(id.Value);

            if (person != null)
            {
                // Create and configure an 'edit form'
                var editForm = AutoMapper.Mapper.Map<PersonBaseEditFamilyForm>(person);
                //editForm = BuildForm(person, editForm);

                if (person.Father == null)
                {
                    editForm.Father = new SelectList(m.PossibleFathers(person.BirthDate), "Id", "FullName");
                }
                else
                {
                    editForm.Father = new SelectList(m.PossibleFathers(person.BirthDate), "Id", "FullName", person.Father.Id);
                }

                if (person.Mother == null)
                {
                    editForm.Mother = new SelectList(m.PossibleMothers(person.BirthDate), "Id", "FullName");
                }
                else
                {
                    editForm.Mother = new SelectList(m.PossibleMothers(person.BirthDate), "Id", "FullName", person.Mother.Id);
                }

                editForm.Children = new MultiSelectList(m.PossibleChildren(person.BirthDate), "Id", "FullName", person.Children.Select(c => c.Id));

                return View(editForm);
            }
            else
            {
                return HttpNotFound();
            }
        }

        // POST: Persons/FamilyEdit/5
        [HttpPost]
        public ActionResult FamilyEdit(int id, PersonBaseEditFamily newItem)
        {
            if (ModelState.IsValid & id == newItem.Id)
            {
                // Attempt to do the update
                var editedItem = m.PersonEditRelations(newItem);

                if (editedItem == null)
                {
                    // There was a problem updating the object

                    ModelState.AddModelError("modelState", "There was an error. (The incoming data is invalid.)");

                    return RedirectToAction("details", new { id = newItem.Id });
                }
                else
                {
                    // Redirect
                    // Could do other things here
                    return RedirectToAction("details", new { id = editedItem.Id });
                }
            }
            // There was a problem updating the object
            // Re-create an edit form

            ModelState.AddModelError("modelState", "There was an error. (The incoming data is invalid.)");

            return RedirectToAction("details", new { id = newItem.Id });
        }

        /*
        // GET: Persons/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Persons/Create
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
        */

        /*
        // GET: Persons/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Persons/Edit/5
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
        */

        /*
        // GET: Persons/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Persons/Delete/5
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
