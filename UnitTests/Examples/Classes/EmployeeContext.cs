using System.Data.Entity;
using UnitTests.Examples.Interfaces;

namespace UnitTests.Examples.Classes
{
    public class EmployeeContext : DbContext, IEmployeeContext
    {
        public IDbSet<Department> Departments { get; set; }
        public IDbSet<Employee> Employees { get; set; }
    }
}
