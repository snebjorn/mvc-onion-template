using System.Collections.Generic;

namespace Core.DomainModel
{
    public class Course : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}