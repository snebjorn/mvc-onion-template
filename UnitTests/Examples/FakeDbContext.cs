using System;
using Faker;
using UnitTests.Examples.Classes;
using UnitTests.Examples.Controllers;
using Xunit;

namespace UnitTests.Examples
{
    public class FakeDbContext
    {
        // http://romiller.com/2012/02/14/testing-with-a-fake-dbcontext/
        // For replacing a database with a fake DbContext.

        private readonly DepartmentController _controller;

        // Usage of Faker.Net to generate random variables.
        private readonly string _depName1 = Company.Name();
        private readonly string _depName2 = Company.Name();
            
        public FakeDbContext()
        {
            // Create fake context and populate it with the fake data.
            
            var context = new FakeEmployeeContext
            {
                Departments =
                {
                    new Department {Name = _depName1 },
                    new Department {Name = _depName2 }
                }
            };
            
            _controller = new DepartmentController(context);
        }

        [Fact]
        public void ContextDepartmentNameTest()
        {
            var res = _controller.Index();

            Assert.True( res.FindIndex(d => d.Name == _depName1) > -1 );
            Assert.True( res.FindIndex(d => d.Name == _depName2) > -1 );
        }

    }
}

