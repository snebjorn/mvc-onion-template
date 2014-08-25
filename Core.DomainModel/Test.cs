using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DomainModel
{
    public class Test : IIdentifier, ICreatedOn, IModifiedOn
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }

    public class Source
    {
        public int MyValue { get; set; }
        public NestedSource  NestedSource { get; set; }
    }

    public class NestedSource
    {
        public string Name { get; set; }
    }

    public class Destination
    {
        public int MyValue { get; set; }
        public string NestedSourceName { get; set; }
    }
}
