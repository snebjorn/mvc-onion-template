﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Core.DomainModel;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Infrastructure.Data
{
    public class SampleContext : DbContext //IdentityDbContext<ApplicationUser>
    {
        public SampleContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer<SampleContext>(new SampleSeedInitializer());
        }

        public IDbSet<Test> Tests { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // use conventions when possible
        }
    }
}
