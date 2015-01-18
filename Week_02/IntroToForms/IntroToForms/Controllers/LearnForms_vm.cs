using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntroToForms.Controllers
{
    public class Person
    {
        public int Id { get; set; }
        public string GivenNames { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }

    // View model to pass to the view
    public class PersonCollection
    {
        public PersonCollection()
        {
            this.Persons = new List<Person>();
        }

        public ICollection<Person> Persons { get; set; }
        // Status message to pass along
        public string StatusMessage { get; set; }
    }
}
