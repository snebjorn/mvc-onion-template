using System.Data.Entity.Migrations;
using System.Linq;
using System.Data.Entity;
using Infrastructure.DataAccess.Seeding;

namespace Infrastructure.DataAccess
{
    public class CreateSampleSeedInitializer : CreateDatabaseIfNotExists<SampleContext>
    {
        protected override void Seed(SampleContext context)
        {
            SeedHelper.Seed(context);
        }
    }

    public class ChangeSampleSeedInitializer : DropCreateDatabaseIfModelChanges<SampleContext>
    {
        public override void InitializeDatabase(SampleContext context)
        {
            // If the database already exist, you may stumble into the case of having an error.
            // The exception “Cannot drop database because it is currently in use” can raise. 
            // This problem occurs when an active connection remains connected to the database that it is in the process of being deleting. 
            // A trick is to override the InitializeDatabase method and to alter the database. 
            // This tell the database to close all connection and if a transaction is open to rollback this one.
            context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction,
                string.Format("ALTER DATABASE {0} SET SINGLE_USER WITH ROLLBACK IMMEDIATE",
                    context.Database.Connection.Database));
            base.InitializeDatabase(context);
        }

        protected override void Seed(SampleContext context)
        {
            SeedHelper.Seed(context);
        }
    }

    public static class SeedHelper
    {
        public static void Seed(SampleContext context)
        {
            for (var i = 0; i <= 10; i++)
            {
                context.Students.AddOrUpdate(SeedingHelper.Student());
            }
            context.SaveChanges();

            for (var i = 0; i <= 2; i++)
            {
                context.Courses.AddOrUpdate(SeedingHelper.Course(context.Students.ToList()));
            }
            context.SaveChanges();

            for (var i = 0; i <= 2; i++)
            {
                context.Teachers.AddOrUpdate(SeedingHelper.Teacher(context.Courses.ToList()));
            }
            context.SaveChanges();

            context.ClassRooms.AddOrUpdate(SeedingHelper.ClassRoom(context.Courses.FirstOrDefault()));
            context.ClassRooms.AddOrUpdate(SeedingHelper.ClassRoom(context.Courses.ToList().Last()));
            context.SaveChanges();
        }
    }
}
