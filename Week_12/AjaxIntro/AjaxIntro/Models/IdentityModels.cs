using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
// added...
using CsvHelper;
using System.IO;
using System.Web;

namespace AjaxIntro.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
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
        public ApplicationDbContext()
            : base("DataContext", throwIfV1Schema: false) { }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Region> Regions { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

    public class StoreInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            // Countries

            var ca = new Country { Name = "Canada" };
            context.Countries.Add(ca);

            var de = new Country { Name = "Germany" };
            context.Countries.Add(de);

            var us = new Country { Name = "United States" };
            context.Countries.Add(us);

            context.SaveChanges();

            // Load Canada

            var path = HttpContext.Current.Server.MapPath("~/App_Data/canada.csv");

            StreamReader sr = File.OpenText(path);

            var csv = new CsvReader(sr);

            while (csv.Read())
            {
                var region = csv.GetField("Region");
                context.Regions.Add(new Region { Country = ca, Name = region });
            }
            context.SaveChanges();

            sr.Close();
            sr = null;
            csv = null;

            // Load Germany

            path = HttpContext.Current.Server.MapPath("~/App_Data/germany.csv");

            sr = File.OpenText(path);

            csv = new CsvReader(sr);

            while (csv.Read())
            {
                var region = csv.GetField("Region");
                context.Regions.Add(new Region { Country = de, Name = region });
            }
            context.SaveChanges();

            sr.Close();
            sr = null;
            csv = null;

            // Load United States

            path = HttpContext.Current.Server.MapPath("~/App_Data/unitedstates.csv");

            sr = File.OpenText(path);

            csv = new CsvReader(sr);

            while (csv.Read())
            {
                var region = csv.GetField("Region");
                context.Regions.Add(new Region { Country = us, Name = region });
            }
            context.SaveChanges();

            sr.Close();
            sr = null;
            csv = null;

        }
    }
}