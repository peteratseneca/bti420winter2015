using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Collections.Controllers
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
}
