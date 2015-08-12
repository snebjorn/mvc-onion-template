using System.Collections.Generic;

namespace UnitTests.Examples.Classes
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
