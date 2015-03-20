using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
// added...
using System.ComponentModel.DataAnnotations;

namespace SecurityIntro.Models
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
            // Replace the following statement with your own code
            base.Seed(context);

            // Activate or deactivate the statement in the 
            // MvcApplication class' Application_Start() method
        }
    }

}