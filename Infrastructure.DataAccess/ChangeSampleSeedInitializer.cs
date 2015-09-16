using System.Data.Entity;

namespace Infrastructure.DataAccess
{
    public class ChangeSampleSeedInitializer : DropCreateDatabaseIfModelChanges<SampleContext>
    {
        public override void InitializeDatabase(SampleContext context)
        {
            // If the database already exist, you may stumble into the case of having an error.
            // The exception “Cannot drop database because it is currently in use” can raise. 
            // This problem occurs when an active connection remains connected to the database that it is in the process of being deleting. 
            // A trick is to override the InitializeDatabase method and to alter the database. 
            // This tell the database to close all connection and if a transaction is open to rollback this one.
            context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction,
                string.Format("ALTER DATABASE {0} SET SINGLE_USER WITH ROLLBACK IMMEDIATE",
                    context.Database.Connection.Database));
            base.InitializeDatabase(context);
        }

        protected override void Seed(SampleContext context)
        {
            SeedHelper.Seed(context);
        }
    }
}
