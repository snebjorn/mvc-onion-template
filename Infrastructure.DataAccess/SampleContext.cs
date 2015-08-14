using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Core.DomainModel;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Infrastructure.Data
{
    public class SampleContext : IdentityDbContext<ApplicationUser>
    {
        public SampleContext()
            : base("DefaultConnection")
        {
        }

        public IDbSet<Student> Students { get; set; }
        public IDbSet<Teacher> Teachers { get; set; } 

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // use conventions when possible
        }
    }
}
