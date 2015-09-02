using System.Data.Entity;

namespace Infrastructure.DataAccess
{
    public class CreateSampleSeedInitializer : CreateDatabaseIfNotExists<SampleContext>
    {
        protected override void Seed(SampleContext context)
        {
            SeedHelper.Seed(context);
        }
    }
}
