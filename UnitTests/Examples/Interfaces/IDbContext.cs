using System.Data.Entity;
using UnitTests.Examples.Classes;

namespace UnitTests.Examples.Interfaces
{
    public interface IDbContext
    {
        IDbSet<Department> Departments { get; }
        IDbSet<Employee> Employees { get; }
        void SaveChanges();
    }
}
