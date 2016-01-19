using System.Data.Entity;

namespace Infrastructure.DataAccess
{
    public class CreateSampleSeedInitializer : CreateDatabaseIfNotExists<ApplicationContext>
    {
        protected override void Seed(ApplicationContext context)
        {
            SeedHelper.Seed(context);
        }
    }
}
