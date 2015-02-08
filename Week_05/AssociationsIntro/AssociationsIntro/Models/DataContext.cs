using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AssociationsIntro.Models
{
    public class DataContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public DataContext()
            : base("name=DataContext")
        {
        }

        public System.Data.Entity.DbSet<AssociationsIntro.Models.Program> Programs { get; set; }
        public DbSet<Subject> Subjects { get; set; }

    }

    public class StoreInitializer : CreateDatabaseIfNotExists<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            var cpa = new Program { Code = "CPA", Credential = "Diploma", Name = "Computer Programming and Analysis", Semesters = 6 };
            context.Programs.Add(cpa);

            var cns = new Program { Code = "CNS", Credential = "Diploma", Name = "Computer Networking and Technical Support", Semesters = 4 };
            context.Programs.Add(cns);

            var pmc = new Program { Code = "PMC", Credential = "Graduate Certificate", Name = "IT Project Management", Semesters = 2 };
            context.Programs.Add(pmc);

            var bsd = new Program { Code = "BSD", Credential = "Degree", Name = "Software Development", Semesters = 8 };
            context.Programs.Add(bsd);

            var btp305 = new Subject { Code = "BTP305", Description = "coming soon", Name = "Object-oriented Software Development Using C++", Program = bsd, Topics = "coming soon" };
            context.Subjects.Add(btp305);

            var btd310 = new Subject { Code = "BTD310", Description = "coming soon", Name = "SQL Database Design Using Oracle", Program = bsd, Topics = "coming soon" };
            context.Subjects.Add(btd310);

            var bti320 = new Subject { Code = "BTI320", Description = "coming soon", Name = "Web Programming on UNIX", Program = bsd, Topics = "coming soon" };
            context.Subjects.Add(bti320);

            var bts330 = new Subject { Code = "BTS330", Description = "coming soon", Name = "Business Requirements Using OO Models", Program = bsd, Topics = "coming soon" };
            context.Subjects.Add(bts330);

            var btc340 = new Subject { Code = "BTC340", Description = "coming soon", Name = "Business Presentations", Program = bsd, Topics = "coming soon" };
            context.Subjects.Add(btc340);

            var btp400 = new Subject { Code = "BTP400", Description = "coming soon", Name = "Object-Oriented Software Development II - Java", Program = bsd, Topics = "coming soon" };
            context.Subjects.Add(btp400);

            var btn415 = new Subject { Code = "BTN415", Description = "coming soon", Name = "Data Communications Programming", Program = bsd, Topics = "coming soon" };
            context.Subjects.Add(btn415);

            var bti420 = new Subject { Code = "BTI420", Description = "coming soon", Name = "Web Programming on Windows", Program = bsd, Topics = "coming soon" };
            context.Subjects.Add(bti420);

            var bts430 = new Subject { Code = "BTS430", Description = "coming soon", Name = "System Analysis and Design Using UML", Program = bsd, Topics = "coming soon" };
            context.Subjects.Add(bts430);

            var btc440 = new Subject { Code = "BTC440", Description = "coming soon", Name = "Business and Technical Writing", Program = bsd, Topics = "coming soon" };
            context.Subjects.Add(btc440);

            context.SaveChanges();
        }
    }

}
