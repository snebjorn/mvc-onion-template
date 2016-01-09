using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

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


    /// <summary>
    /// You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        // Move this method somewhere else.
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
