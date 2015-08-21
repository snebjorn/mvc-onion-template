using System;

namespace Core.DomainModel
{
    public interface IModifiedOn
    {
        DateTime ModifiedOn { get; set; }
    }
}
