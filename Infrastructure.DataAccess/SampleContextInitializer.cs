using System.Data.Entity.Migrations;
using System.Linq;
using Infrastructure.Data.Seeding;

namespace Infrastructure.Data
{
    public class SampleInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<SampleContext>
    {
        /// <summary>
        /// Specify what the database should be seeded with here.
        /// </summary>
        /// <param name="context">The database context.</param>
        protected override void Seed(SampleContext context)
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
