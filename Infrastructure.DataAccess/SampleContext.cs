using System.Security.Claims;
using System.Threading.Tasks;
using System.Data.Entity;
using Core.DomainModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Infrastructure.Data
{
    public class SampleContext : IdentityDbContext<ApplicationUser>
    {

        // throwIfV1Schema is used when updgrading Identity in a database from 1 to 2.
        // It's a one time thing and can be safely removed.
        public SampleContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {

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
            // Use conventions when possible.
            modelBuilder.Entity<Student>().Property(s => s.CreatedOn).HasColumnType("datetime2");
            modelBuilder.Entity<Student>().Property(s => s.ModifiedOn).HasColumnType("datetime2");

            // Use a maxlength on strings, because if somebody decides to submit a very large string, then the local database will 
            // allocate a very large area, which cannot be used when generating indexes.
            modelBuilder.Entity<Student>().Property(s => s.Name).HasMaxLength(50);
            modelBuilder.Entity<Course>().Property(s => s.Name).HasMaxLength(50);
            modelBuilder.Entity<Teacher>().Property(s => s.Name).HasMaxLength(50);


            //modelBuilder.Entity<>()

            base.OnModelCreating(modelBuilder);
        }
    }
}
