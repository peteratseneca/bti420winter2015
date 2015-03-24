using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
// added...
using System.ComponentModel.DataAnnotations;
using System.IO;
using CsvHelper;
using AutoMapper;

namespace Family.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("DataContext", throwIfV1Schema: false) { }

        // Add your DbSet<TEntity> properties here
        public DbSet<Person> Persons { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

    public class StoreInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        // Other superclasses include
        // DropCreateDatabaseAlways<context>
        // DropCreateDatabaseIfModelChanges<context>

        protected override void Seed(ApplicationDbContext context)
        {
            // If no data, create some data
            if (context.Persons.CountAsync().Result == 0)
            {
                // File system path to the data file (in this project's App_Data folder)
                string path = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/People.csv");

                // Create a stream reader object, to read from the file system
                StreamReader sr = File.OpenText(path);

                // Create the CsvHelper object
                var csv = new CsvReader(sr);

                // Go through the data file
                while (csv.Read())
                {
                    // Read into an object
                    var p = csv.GetRecord<Controllers.PersonAddFromCsv>();

                    // Add it to the data store
                    context.Persons.Add(Mapper.Map<Models.Person>(p));
                }

                // Clean up
                sr.Close();
                sr = null;

                context.SaveChanges();
            }

            // Activate or deactivate the statement in the 
            // MvcApplication class' Application_Start() method
        }
    }

}