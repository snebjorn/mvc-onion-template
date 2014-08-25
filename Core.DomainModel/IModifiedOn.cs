using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DomainModel
{
    public interface IModifiedOn
    {
        DateTime ModifiedOn { get; set; }
    }
}
