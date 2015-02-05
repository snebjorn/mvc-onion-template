using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DomainModel
{
    public class Student : IEntity, ICreatedOn, IModifiedOn
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }

    public class Course : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public virtual Teacher Teacher { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        //public virtual ClassRoom ClassRoom { get; set; }
    }

    public class Teacher : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }

    public class ClassRoom : IEntity
    {
        public int Id { get; set; }
        public int BuildingNumber { get; set; }
        public int Floor { get; set; }
        public virtual Course Course { get; set; }
    }
}
