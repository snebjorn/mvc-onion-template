using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DomainServices
{
    public interface IUnitOfWork
    {
        int Save();
        Task<int> SaveAsync();
    }
}
