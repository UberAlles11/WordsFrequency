using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using WordsFrequency.Common.DAL.Entities;

namespace WordsFrequency.Common.DAL
{

    public class DataContext: DbContext {

        public DbSet<WordsCountBase> WordsCount { get; set; }
        public DbSet<SourceTextBase> SourceText { get; set; }

        public DataContext()
: base()//"WordsFrequencyData")
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext, WordsFrequency.Migrations.Configuration>("WordsFrequency"));
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
