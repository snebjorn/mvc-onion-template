using Core.DomainModel;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;

namespace Infrastructure.DataAccess
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        // throwIfV1Schema is used when upgrading Identity in a database from 1 to 2.
        // It's a one time thing and can be safely removed.
        public ApplicationContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationContext Create()
        {
            return new ApplicationContext();
        }

        // Define you conceptual model here. Entity Framework will include these types and all their references.
        // There are 3 different inheritance strategies:
        // * Table per Hierarchy (TPH) (default): http://weblogs.asp.net/manavi/inheritance-mapping-strategies-with-entity-framework-code-first-ctp5-part-1-table-per-hierarchy-tph
        // * Table per Type (TPT): http://weblogs.asp.net/manavi/inheritance-mapping-strategies-with-entity-framework-code-first-ctp5-part-2-table-per-type-tpt
        // * Table per Concrete class (TPC): http://weblogs.asp.net/manavi/inheritance-mapping-strategies-with-entity-framework-code-first-ctp5-part-3-table-per-concrete-type-tpc-and-choosing-strategy-guidelines

        public IDbSet<Student> Students { get; set; }
        public IDbSet<Teacher> Teachers { get; set; }
        public IDbSet<Course> Courses { get; set; }
        public IDbSet<ClassRoom> ClassRooms { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // The DateTime type in .NET has the same range and precision as datetime2 in SQL Server.
            // Configure DateTime type to use SQL server datetime2 instead.
            modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));

            // Consider setting max length on string properties.
            // http://dba.stackexchange.com/questions/48408/ef-code-first-uses-nvarcharmax-for-all-strings-will-this-hurt-query-performan
            modelBuilder.Properties<string>().Configure(c => c.HasMaxLength(100));

            // Set max length where it makes sense.
            modelBuilder.Entity<Student>().Property(s => s.Name).HasMaxLength(50);
            modelBuilder.Entity<Teacher>().Property(s => s.Name).HasMaxLength(50);
            modelBuilder.Entity<Course>().Property(s => s.Name).HasMaxLength(30);

            base.OnModelCreating(modelBuilder);
        }
    }
}
