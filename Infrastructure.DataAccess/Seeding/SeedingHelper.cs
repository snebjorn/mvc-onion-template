using System;
using System.Collections.Generic;
using Core.DomainModel;
using Faker;

namespace Infrastructure.DataAccess.Seeding
{
    /// <summary>
    /// Examples of functions that uses Faker.Net to generate data and 
    /// seed a context with either departments or employees.
    /// The functions uses a interface as argument, so either give your context this interface
    /// or change the class, and adapt/change the functions to match the context you want to seed.
    /// </summary>
    public class SeedingHelper
    {
        public static Student Student()
        {
            return new Student()
            {
                Name = Name.First(), 
                CreatedOn = DateTime.Now, 
                ModifiedOn = DateTime.Now
            };
        }

        public static Course Course(List<Student> students)
        {
            return new Course()
            {
                Name = Company.Suffix(),
                Students = students
            };
        }

        public static Teacher Teacher(List<Course> courses)
        {
            return new Teacher()
            {
                Name = Name.First(),
                Courses = courses
            };

        }

        public static ClassRoom ClassRoom(Course course)
        {
            return new ClassRoom()
            {
                BuildingNumber = RandomNumber.Next(),
                Floor = RandomNumber.Next(),
                Course = course
            };
        }
    }
}
