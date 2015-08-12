using System.Collections.Generic;
using System.Linq;
using UnitTests.Examples.Classes;
using UnitTests.Examples.Interfaces;

namespace UnitTests.Examples.Controllers
{
    public class DepartmentController
    {
        private readonly IEmployeeContext dbContext;

        public DepartmentController()
        {
            this.dbContext = new EmployeeContext();
        }

        public DepartmentController(IEmployeeContext context)
        {
            this.dbContext = context;
        }

        public List<Department> Index()
        {
            return dbContext.Departments.OrderBy(d => d.Name).ToList();
        }

        public List<Employee> IndexEmployees()
        {
            return dbContext.Employees.OrderBy(e => e.FirstName).ToList();
        } 
    }
}
