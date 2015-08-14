using Core.DomainModel;
using System;
using System.Data.Entity;

namespace Infrastructure.Data
{
    public class CreateSampleSeedInitializer : CreateDatabaseIfNotExists<SampleContext>
    {
        protected override void Seed(SampleContext context)
        {
            SeedHelper.Seed(context);
        }
    }

    public class ChangeSampleSeedInitializer : DropCreateDatabaseIfModelChanges<SampleContext>
    {
        protected override void Seed(SampleContext context)
        {
            SeedHelper.Seed(context);
        }
    }

    public static class SeedHelper
    {
        public static void Seed(SampleContext context)
        {
            var item = new Student() { Name = Faker.Name.First(), CreatedOn = DateTime.Now, ModifiedOn = DateTime.Now };
            context.Students.Add(item);
            context.SaveChanges();
        }
    }
}
