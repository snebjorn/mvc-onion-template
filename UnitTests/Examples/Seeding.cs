
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using UnitTests.Examples.Classes;
using UnitTests.Examples.Interfaces;

namespace UnitTests.Examples
{
    public class Seeding
    {
        public Seeding()
        {
            //Seeding.SeedDepartments(context);
        }

        // Adds a department to the context.
        public static void SeedDepartment(IDbContext context)
        {
            context.Departments.AddOrUpdate(
                 d => d.Name,
                    new Department { Name = Faker.Company.Name(), DepartmentId = Faker.RandomNumber.Next() }
                 );

            context.SaveChanges();
        }

        // Adds departments to the context.
        public static void SeedDepartments(IDbContext context)
        {
            context.Departments.AddOrUpdate(
                d => d.Name,
                    Enumerable.Range(1, 10).Select(x => new Department { Name = Faker.Company.Name(), DepartmentId = Faker.RandomNumber.Next() }).ToArray()
                );

            context.SaveChanges();
        }
    }
}
