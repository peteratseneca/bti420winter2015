using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
// added...
using System.ComponentModel.DataAnnotations;

namespace Notes.Models
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

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<OU> OUs { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

    public class StoreInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        // Other superclasses include
        // DropCreateDatabaseAlways<DbContextClassName>
        // DropCreateDatabaseIfModelChanges<DbContextClassName>


        // The code below was copied to the Configuration class in the Migrations namespace


        protected override void Seed(ApplicationDbContext context)
        {
            // Add initial 'admin' user...

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

            // Replace the following statement with your own code
            //base.Seed(context);

            // Create a reference to the data manager
            Controllers.Manager m = new Controllers.Manager();
            // Load the OUs
            m.LoadOUs();
            // Load the Employees
            m.LoadEmployeesFromCSV();

            // Activate or deactivate the statement in the 
            // MvcApplication class' Application_Start() method
        }
    }

}