using System.Collections.Generic;
using Infrastructure.Data.Seeding;
using Core.DomainModel;

namespace Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Infrastructure.Data.SampleContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Infrastructure.Data.SampleContext context)
        {
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

            if (!context.Teachers.Any()) {
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
        }
    }
}
