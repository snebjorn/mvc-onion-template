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
            // Database.SetInitializer<ApplicationContext>(null);
            // The database initializer will create the database and the specified tables.
            // If you're using an existing database with code first, don't execute any logic at all.

            // Database.SetInitializer(new CreateDatabaseIfNotExists<ApplicationContext>()); // Default, will run if nothing is done.
            // The default option. When the application runs the first time, entity framework will create
            // a code first database if it does not already exist. If the database exists and you have done modifications
            // this will throw an InvalidOperationException.

            // Database.SetInitializer(new DropCreateDatabaseAlways<ApplicationContext>());
            // This option, as the name suggests, will always drop and recreate the database when the application runs the first time.
            // All tables will be deleted as the database is dropped.

            // Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ApplicationContext>());
            // This option will drop and recreate the database if there are any changes to the model.

            // Alternatively, you can create your own initializer and pass it as the argument.
            // The class will need to implement one of the above in order to inherit some behaviour.
            // These Initializers has a custom seeding function, that will populate the database instance
            // with some fake data that can be used right away.
            if (Database.Exists("DefaultConnection"))
            {
                // Runs if a database already exists. It drops the database, recreates it and seeds with some fake data.
                Database.SetInitializer(new ChangeSampleSeedInitializer());
            }
            else
            {
                // This initilizer will run if there are no database.
                // Usually this is on the first run, or if the database was deleted.
                // Seeds with some fake data.
                Database.SetInitializer(new CreateSampleSeedInitializer());
            }
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
