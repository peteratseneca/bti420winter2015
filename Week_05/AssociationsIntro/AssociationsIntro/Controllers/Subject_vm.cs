using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssociationsIntro.Controllers
{
    public class SubjectAdd
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Topics { get; set; }
    }

    public class SubjectBase : SubjectAdd
    {
        public int Id { get; set; }
    }

    public class SubjectBaseWithProgram : SubjectBase
    {
        public ProgramBase Program { get; set; }
    }

}
