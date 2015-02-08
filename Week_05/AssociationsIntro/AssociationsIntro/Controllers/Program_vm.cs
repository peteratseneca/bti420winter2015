using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssociationsIntro.Controllers
{
    public class ProgramAdd
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Credential { get; set; }
        public int Semesters { get; set; }
    }

    public class ProgramBase : ProgramAdd
    {
        public int Id { get; set; }
    }

    public class ProgramBaseWithSubjects : ProgramBase
    {
        public ProgramBaseWithSubjects()
        {
            this.Subjects = new List<SubjectBase>();
        }

        public IEnumerable<SubjectBase> Subjects { get; set; }
    }

}
