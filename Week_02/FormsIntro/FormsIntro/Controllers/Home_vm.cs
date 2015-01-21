using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FormsIntro.Controllers
{
    // The PersonAdd view model class describes the shape of a "new" person
    // It does not have an identifier, because the data store will assign this value

    public class PersonAdd
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }

    // The PersonFull view model class describes the a complete person, with all properties
    // Notice that it inherits from PersonAdd

    public class PersonFull : PersonAdd
    {
        public int Id { get; set; }
    }
}
