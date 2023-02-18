using Stargazer.Abp.ObjectStorage.Domain;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Stargazer.Abp.ObjectStorage.EntityFrameworkCore
{    
    [ConnectionStringName("Default")]
    public class EntityFrameworkCoreDbContext: AbpDbContext<EntityFrameworkCoreDbContext>
    {

        public DbSet<ObjectData> ObjectDatas { get; set; }

        public EntityFrameworkCoreDbContext(DbContextOptions<EntityFrameworkCoreDbContext> options)
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