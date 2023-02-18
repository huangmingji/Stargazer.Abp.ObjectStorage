using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Stargazer.Abp.ObjectStorage.EntityFrameworkCore.DbMigrations
{    
    [ConnectionStringName("Default")]
    public class DbMigrationsDbContext: AbpDbContext<DbMigrationsDbContext>
    {
        public DbMigrationsDbContext(DbContextOptions<DbMigrationsDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ConfigureObjectStorage();
            base.OnModelCreating(builder);
        }

    }
}