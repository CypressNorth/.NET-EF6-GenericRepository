namespace Data.Repository.Context
{
    using System.Data.Entity;

    public class AppDbInitializer : DropCreateDatabaseIfModelChanges<AppDbContext>
    {
        protected override void Seed(AppDbContext context)
        {
            /**
             *  Seed db here ..  suitable for early development.
             *  Consider Migrations afterward.
             */

            base.Seed(context);
        }
    }
}