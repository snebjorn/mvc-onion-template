using System.Data.Entity;

namespace Infrastructure.DataAccess
{
    public class ChangeSampleSeedInitializer : DropCreateDatabaseIfModelChanges<ApplicationContext>
    {
        public override void InitializeDatabase(ApplicationContext context)
        {
            // If the database already exist, you may stumble into the following error.
            // The exception “Cannot drop database because it is currently in use” can raise.
            // This problem occurs when an active connection remains connected to the database that it is in the process of being deleted.
            // A trick is to override the InitializeDatabase method and to alter the database.
            // This tells the database to close all connections and to rollback any open transactions.
            context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction,
                string.Format("ALTER DATABASE [{0}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE",
                    context.Database.Connection.Database));
            base.InitializeDatabase(context);
        }

        protected override void Seed(ApplicationContext context)
        {
            SeedHelper.Seed(context);
        }
    }
}
