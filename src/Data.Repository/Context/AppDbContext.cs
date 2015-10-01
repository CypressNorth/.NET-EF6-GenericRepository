namespace Data.Repository.Context
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class AppDbContext : DbContext
    {
        #region Properties "Tables"

        public DbSet<SampleEntity> Samples { get; set; }

        #endregion

        #region Constructors

        static AppDbContext()
        {
            Database.SetInitializer<AppDbContext>(new AppDbInitializer());
        }

        public AppDbContext()
            : base("name=DefaultConnection")
        { }

        #endregion Constructors

        #region Methods

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /**
             *  Db Configurations ..
             */
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            this.Configuration.LazyLoadingEnabled = true;

            /**
             *  Draw and map relationships via Fluent API.
             */
            SetEntityRelations(modelBuilder);
        }

        private void SetEntityRelations(DbModelBuilder modelBuilder)
        {

        }

        #endregion Methods
    }
}