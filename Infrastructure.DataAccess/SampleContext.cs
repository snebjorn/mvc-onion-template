using System.Security.Claims;
using System.Threading.Tasks;
using System.Data.Entity;
using Core.DomainModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Infrastructure.Data
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Data.ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class SampleContext : IdentityDbContext<ApplicationUser>
    {
        public SampleContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            // Database.SetInitializer<SampleContext>(null);
            // The database initializer will create the database and the specified tables. 
            // If you're usin an existing database with code first, don't execute any logic at all.

            // Database.SetInitializer(new CreateDatabaseIfNotExists<SampleContext>()); // Default
            // The default option. When the application runs the first time, entity framework will create
            // a code first database if it does not already exist. If the database exists and you have done modifications
            // this will throw an InvalidOperationException.

            // Database.SetInitializer(new DropCreateDatabaseAlways<SampleContext>());
            // This option, as the name suggests, will always drop and recreate the database when the application runs the first time.
            // All tables will be deleted as the database is dropped.

            // Database.SetInitializer(new DropCreateDatabaseIfModelChanges<SampleContext>());
            // This option will drop and recreate the database if there are any changes to the model.

            // Alternatively, you can create your own initializer and pass it as the argument.
            // The class will need to implement one of the above in order to inherit some behaviour.
            Database.SetInitializer(new SampleInitializer());
        }

        public static SampleContext Create()
        {
            return new SampleContext();
        }

        // Define you conceptual model here. Code first will include these types and all their references. 
        public IDbSet<Student> Students { get; set; }
        public IDbSet<Teacher> Teachers { get; set; }
        public IDbSet<Course> Courses { get; set; }
        public IDbSet<ClassRoom> ClassRooms { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // use conventions when possible
            base.OnModelCreating(modelBuilder);
        }
    }
}

// OLD CODE

/*
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
        : base("DefaultConnection", throwIfV1Schema: false)
    {
    }

    public static ApplicationDbContext Create()
    {
        return new ApplicationDbContext();
    }
}
*/


