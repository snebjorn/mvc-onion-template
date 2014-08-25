using Core.DomainModel;
using System;
using System.Data.Entity;

namespace Infrastructure.Data
{
    public class SampleSeedInitializer : DropCreateDatabaseIfModelChanges<SampleContext>
    {
        protected override void Seed(SampleContext context)
        {
            var item = new Test() { CreatedOn = DateTime.Now, ModifiedOn = DateTime.UtcNow };
            context.Tests.Add(item);
            context.SaveChanges();
        }
    }
}
