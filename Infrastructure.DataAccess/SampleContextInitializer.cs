using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Data.Seeding;

namespace Infrastructure.Data
{
    public class SampleInitializer : System.Data.Entity.DropCreateDatabaseAlways<SampleContext>
    {
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

            if (!context.Courses.Any())
            {
                for (var i = 0; i <= 2; i++)
                {
                    context.Courses.AddOrUpdate(SeedingHelper.Course(context.Students.ToList()));
                }
                context.SaveChanges();
            }

            if (!context.Teachers.Any())
            {
                for (var i = 0; i <= 2; i++)
                {
                    context.Teachers.AddOrUpdate(SeedingHelper.Teacher(context.Courses.ToList()));
                }
                context.SaveChanges();
            }

            if (!context.ClassRooms.Any())
            {
                context.ClassRooms.AddOrUpdate(SeedingHelper.ClassRoom(context.Courses.FirstOrDefault()));
                context.ClassRooms.AddOrUpdate(SeedingHelper.ClassRoom(context.Courses.ToList().Last()));
                context.SaveChanges();
            }  
            
            //base.Seed(context);
        }
    }
}
