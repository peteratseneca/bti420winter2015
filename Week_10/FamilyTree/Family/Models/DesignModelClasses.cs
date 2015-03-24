using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// added...
using System.ComponentModel.DataAnnotations;

namespace Family.Models
{
    public class Person
    {
        public Person()
        {
            this.BirthDate = DateTime.Now;
            this.Gender = "Male";
            this.Children = new List<Person>();
        }

        public int Id { get; set; }

        [Required, StringLength(100)]
        public string FamilyName { get; set; }

        [Required, StringLength(100)]
        public string GivenNames { get; set; }

        [Required, StringLength(10)]
        public string Gender { get; set; }

        public DateTime BirthDate { get; set; }

        // Self-referencing one-to-one
        // Two properties are REQUIRED
        public int? FatherId { get; set; }
        public Person Father { get; set; }

        // Self-referencing one-to-one
        // Two properties are REQUIRED
        public int? MotherId { get; set; }
        public Person Mother { get; set; }

        public ICollection<Person> Children { get; set; }
    }
}
