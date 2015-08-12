using System.Data.Entity;
using UnitTests.Examples.Classes;

namespace UnitTests.Examples.Interfaces
{
    public interface IEmployeeContext
    {
        IDbSet<Department> Departments { get; }
        IDbSet<Employee> Employees { get; }
        int SaveChanges();
    }
}
