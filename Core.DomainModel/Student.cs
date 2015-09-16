using System;
using System.Collections.Generic;

namespace Core.DomainModel
{
    public class Student : IEntity, ICreatedOn, IModifiedOn
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public float AverageGrade { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}
