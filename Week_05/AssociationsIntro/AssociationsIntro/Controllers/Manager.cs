using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// added...
using AutoMapper;
using AssociationsIntro.Models;

namespace AssociationsIntro.Controllers
{
    public class Manager
    {
        private DataContext ds = new DataContext();

        public IEnumerable<ProgramBase> AllPrograms()
        {
            var fetchedObjects = ds.Programs.OrderBy(cd => cd.Code);

            return Mapper.Map<IEnumerable<ProgramBase>>(fetchedObjects);
        }

        public IEnumerable<ProgramBase> AllProgramsFiltered()
        {
            var fetchedObjects = ds.Programs
                .Where(rv => rv.Credential == "Diploma")
                .OrderBy(cd => cd.Code);

            return Mapper.Map<IEnumerable<ProgramBase>>(fetchedObjects);
        }

        public IEnumerable<ProgramBaseWithSubjects> AllProgramsWithSubjectsFiltered()
        {
            var fetchedObjects = ds.Programs
                .Include("Subjects")
                .Where(prog=>prog.Subjects.Any(subj=>subj.Name.ToLower().Contains("prog")))
                .OrderBy(cd => cd.Code);

            return Mapper.Map<IEnumerable<ProgramBaseWithSubjects>>(fetchedObjects);
        }

    }

}
