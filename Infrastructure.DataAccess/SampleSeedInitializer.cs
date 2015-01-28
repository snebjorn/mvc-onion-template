using Core.DomainModel;
using System;
using System.Data.Entity;

namespace Infrastructure.Data
{
    public class SampleSeedInitializer : DropCreateDatabaseAlways<SampleContext>
    {
        protected override void Seed(SampleContext context)
        {
            var item = new Test() { CreatedOn = DateTime.Now, ModifiedOn = DateTime.Now };
            context.Tests.Add(item);
            context.SaveChanges();
        }
    }
}
