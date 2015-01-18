using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersistDataInMemory.Controllers
{
    public class Person
    {
        public Person()
        {
            this.FavouriteColours = new List<string>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<string> FavouriteColours { get; set; }
    }

    // Customized view model to pass to the About view after Session State round trip
    public class AboutViewModel
    {
        public AboutViewModel()
        {
            this.Colours = new List<string>();
            this.Teachers = new List<Person>();
        }

        public int MyAge { get; set; }
        public string MyName { get; set; }
        public ICollection<string> Colours { get; set; }
        public ICollection<Person> Teachers { get; set; }
    }
}
