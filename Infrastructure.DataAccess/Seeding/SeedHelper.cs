using System.Data.Entity.Migrations;
using System.Linq;

namespace Infrastructure.DataAccess.Seeding
{
    internal static class SeedHelper
    {
        public static void Seed(ApplicationContext context)
        {
            for (var i = 0; i <= 10; i++)
            {
                context.Students.AddOrUpdate(FakeDataHelper.GetStudent());
            }
            context.SaveChanges();

            for (var i = 0; i <= 2; i++)
            {
                context.Courses.AddOrUpdate(FakeDataHelper.GetCourse(context.Students.ToList()));
            }
            context.SaveChanges();

            for (var i = 0; i <= 2; i++)
            {
                context.Teachers.AddOrUpdate(FakeDataHelper.GetTeacher(context.Courses.ToList()));
            }
            context.SaveChanges();

            context.ClassRooms.AddOrUpdate(FakeDataHelper.GetClassRoom(context.Courses.FirstOrDefault()));
            context.ClassRooms.AddOrUpdate(FakeDataHelper.GetClassRoom(context.Courses.ToList().Last()));
            context.SaveChanges();
        }
    }
}
