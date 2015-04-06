namespace Notes.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    // added...
    using System.Security.Claims;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Notes.Models;
    using Notes.Controllers;
    using CsvHelper;
    using AutoMapper;
    using System.IO;

    internal sealed class Configuration : DbMigrationsConfiguration<Notes.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Notes.Models.ApplicationDbContext";
        }

        protected override void Seed(Notes.Models.ApplicationDbContext context)
        {
            // Add the initial 'admin' user...
            if (context.Users.Count() == 0)
            {
                // First, initialize a user manager
                var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                // Attempt to create an 'admin' user
                var admin = new ApplicationUser { FirstName = "App", LastName = "Administrator", UserName = "admin@example.com", Email = "admin@example.com" };
                var result = um.Create(admin, "Password123!");

                // Attempt to add role claims
                if (result.Succeeded)
                {
                    um.AddClaim(admin.Id, new Claim(ClaimTypes.Email, admin.Email));
                    um.AddClaim(admin.Id, new Claim(ClaimTypes.GivenName, admin.FirstName));
                    um.AddClaim(admin.Id, new Claim(ClaimTypes.Surname, admin.LastName));
                    um.AddClaim(admin.Id, new Claim(ClaimTypes.Role, "Administrator"));
                }
            }

            // Add the 'organizational units' and the 'employees'

            // Create a reference to the app's data manager
            Controllers.Manager m = new Controllers.Manager();

            // Load the OUs
            if (context.OUs.Count() == 0)
            {
                // Add some OUs...
                context.OUs.Add(new OU { OUName = "Executive Office" });
                context.OUs.Add(new OU { OUName = "Electrical" });
                context.OUs.Add(new OU { OUName = "Editorial" });
                context.OUs.Add(new OU { OUName = "Research & Development" });
                context.OUs.Add(new OU { OUName = "Talent" });
                context.OUs.Add(new OU { OUName = "Human Resources" });
                context.OUs.Add(new OU { OUName = "Production" });
                context.OUs.Add(new OU { OUName = "Creative" });

                context.SaveChanges();
            }

            // Load the Employees
            if (context.Employees.Count() == 0)
            {
                // File system path to the data file (in this project's App_Data folder)
                // Not yet tested with Azure - if it is problematic...
                // ...contact your professor for help (or with the solution)
                string path = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Employees.csv");

                // Create a stream reader object, to read from the file system
                StreamReader sr = File.OpenText(path);

                // Create the CsvHelper object
                var csv = new CsvReader(sr);

                // Create the AutoMapper mapping
                Mapper.CreateMap<Controllers.EmployeeAdd, Models.Employee>();

                // Go through the data file
                while (csv.Read())
                {
                    // Read one line in the source file 
                    EmployeeAdd newItem = csv.GetRecord<EmployeeAdd>();

                    // Create a new employee object
                    Employee addedItem = Mapper.Map<Employee>(newItem);

                    // Add the new object to the data store
                    context.Employees.Add(addedItem);
                }

                context.SaveChanges();

                // Clean up
                sr.Close();
                sr = null;
            }
        }
    }
}
