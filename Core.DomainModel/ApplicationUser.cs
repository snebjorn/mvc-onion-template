using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DomainModel
{
    /// <remarks>
    /// Deriving from IdentityUser as this level is wrong.
    /// As it needs the Microsoft.AspNet.Identity.EntityFramework namespace,
    /// which conflicts with the rules of onion architecture.
    /// 
    /// A possible solution is to use adapter pattern like so:
    /// http://weblogs.asp.net/imranbaloch/a-sample-of-onion-architecture-with-asp-net-identity
    /// 
    /// However it's not trivial and might cause more confusion then needed, 
    /// and have a feeling the Identity team might change this rather 
    /// tightly couple approach in the future.
    /// </remarks>
    public class ApplicationUser : IdentityUser
    {
    }
}
