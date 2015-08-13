using System.Data.Entity.Migrations;
using System.Linq;
using UnitTests.Examples.Classes;
using UnitTests.Examples.Interfaces;

namespace UnitTests.Examples
{
    public class Seeding
    {
        /*
         * Examples of functions that uses Faker.Net to generate data and 
         * seed a context with either departments or employees.
         * The functions uses a interface as argument, so either give your context this interface
         * or change the class, and adapt/change the functions to match the context you want to seed.
         * 
         * Seeding.SeedDepartments(context);
         * Seeding.SeedEmployee(context);
         */

        // Adds a department to the context.
        public static void SeedDepartment(IDbContext context)
        {
            context.Departments.AddOrUpdate(
                 d => d.Name, // Defined uniquely by its name.
                    new Department
                    {
                        Name = Faker.Company.Name()
                    }
                 );

            context.SaveChanges();
        }

        // Adds departments to the context.
        public static void SeedDepartments(IDbContext context)
        {
            context.Departments.AddOrUpdate(
                d => d.Name,
                    Enumerable.Range(1, 10).Select(x => new Department
                    {
                        Name = Faker.Company.Name()
                    }).ToArray()
                );

            context.SaveChanges();
        }

        // Adds a employee to the context
        public static void SeedEmployee(IDbContext context)
        {
            context.Employees.AddOrUpdate(
                new Employee
                    {
                        FirstName = Faker.Name.First(),
                        LastName = Faker.Name.Last()
                    }
                );

            context.SaveChanges();
        }

        // Adds employees to the context.
        public static void SeedEmployees(IDbContext context)
        {
            context.Employees.AddOrUpdate(
                Enumerable.Range(1, 10).Select(x => new Employee 
                    { 
                        FirstName = Faker.Name.First(), 
                        LastName = Faker.Name.Last() 
                    }).ToArray()
                );

            context.SaveChanges();
        }
    }
}
