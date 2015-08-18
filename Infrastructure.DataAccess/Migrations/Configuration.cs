using Infrastructure.Data.Seeding;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Infrastructure.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<SampleContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        // This function is called on the database each time a Update-Database is called.
        // Usually when seeding the function AddOrUpdate is used, but the function checks 
        // whether the items exists when choosing to either adding or updating.
        // When using Faker.net to generate data, every row of data that is inserted is new,
        // so checking wheter it exists will always be false.
        // 
        protected override void Seed(SampleContext context)
        {
            // 
            if (!context.Students.Any())
            {
                for (var i = 0; i <= 10; i++)
                {
                    context.Students.AddOrUpdate(SeedingHelper.Student());
                }
                context.SaveChanges();
            }

            if (!context.Courses.Any()) {
                for (var i = 0; i <= 2; i++)
                {
                    context.Courses.AddOrUpdate(SeedingHelper.Course(context.Students.ToList()));
                }
                context.SaveChanges();
            }

            context.Students.Clear();

            if (!context.Teachers.Any()) {
                for (var i = 0; i <= 2; i++)
                {
                    context.Teachers.AddOrUpdate(SeedingHelper.Teacher(context.Courses.ToList()));
                }
                context.SaveChanges();
            }

            //context.Students.();

            if (!context.ClassRooms.Any())
            {
                context.ClassRooms.AddOrUpdate(SeedingHelper.ClassRoom(context.Courses.FirstOrDefault()));
                context.ClassRooms.AddOrUpdate(SeedingHelper.ClassRoom(context.Courses.ToList().Last()));
                context.SaveChanges();
            }            
        }
    }
}
