using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssociationsIntro.Models
{
    public class Program
    {
        public Program()
        {
            this.Subjects = new List<Subject>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Credential { get; set; }
        public int Semesters { get; set; }

        public ICollection<Subject> Subjects { get; set; }
    }

    public class Subject
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Topics { get; set; }

        public Program Program { get; set; }
    }

    // Store initializer

}
