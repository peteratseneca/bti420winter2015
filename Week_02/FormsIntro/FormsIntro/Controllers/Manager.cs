using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// These 'using' directives were added...
using System.IO;
using Biggy.Core;
using Biggy.Data.Json;

namespace FormsIntro.Controllers
{
    public class Manager
    {
        // Private fields that point to the data store and the collection(s)
        private JsonStore<PersonFull> store;
        private BiggyList<PersonFull> persons;

        // Public property, read only, with a getter (no setter)
        // Notice the OrderBy method
        // Notice the return type - must be an IEnumerable of a view model type
        public IEnumerable<PersonFull> AllPersons
        {
            get { return persons.OrderBy(ln => ln.LastName).AsEnumerable(); }
        }

        // Default constructor
        public Manager()
        {
            // Path to the app's read-write file system location
            var localData = HttpContext.Current.Server.MapPath("~/App_Data");

            // Open (or create) the data store
            store = new JsonStore<PersonFull>(localData, "company", "persons");
            // Assign (or create) the collection(s)
            persons = new BiggyList<PersonFull>(store);
        }

        // Add a new person
        public PersonFull AddPerson(PersonAdd newItem)
        {
            // Calculate the next value for the identifier
            int newId = (persons.Count > 0) ? newId = persons.Max(id => id.Id) + 1 : 1;

            // Create a new item; notice the property mapping
            var addedItem = new PersonFull
            {
                Id = newId,
                FirstName = newItem.FirstName,
                LastName = newItem.LastName,
                Age = newItem.Age
            };

            // Add the new item to the store
            persons.Add(addedItem);

            // Return the new item
            return addedItem;
        }

    }

}
