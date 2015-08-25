using System.Data.Entity;
using Core.DomainModel;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Infrastructure.Data
{
    public class SampleContext : IdentityDbContext<ApplicationUser>
    {
        // throwIfV1Schema is used when upgrading Identity in a database from 1 to 2.
        // It's a one time thing and can be safely removed.
        public SampleContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {

        }

        public static SampleContext Create()
        {
            return new SampleContext();
        }

        // Define you conceptual model here. Entity Framework will include these types and all their references. 
        public IDbSet<Student> Students { get; set; }
        public IDbSet<Teacher> Teachers { get; set; }
        public IDbSet<Course> Courses { get; set; }
        public IDbSet<ClassRoom> ClassRooms { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().Property(s => s.CreatedOn).HasColumnType("datetime2");
            modelBuilder.Entity<Student>().Property(s => s.ModifiedOn).HasColumnType("datetime2");

            // Consider setting maxlength on string properties, 
            // http://dba.stackexchange.com/questions/48408/ef-code-first-uses-nvarcharmax-for-all-strings-will-this-hurt-query-performan
            modelBuilder.Entity<Student>().Property(s => s.Name).HasMaxLength(50);
            modelBuilder.Entity<Course>().Property(s => s.Name).HasMaxLength(50);
            modelBuilder.Entity<Teacher>().Property(s => s.Name).HasMaxLength(50);

            base.OnModelCreating(modelBuilder);
        }
    }
}
