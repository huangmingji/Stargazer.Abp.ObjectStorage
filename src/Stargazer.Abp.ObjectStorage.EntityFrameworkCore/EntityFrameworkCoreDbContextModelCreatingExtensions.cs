using Stargazer.Abp.ObjectStorage.Domain;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Stargazer.Abp.ObjectStorage.EntityFrameworkCore
{
    public static class EntityFrameworkCoreDbContextModelCreatingExtensions
    {
        public static void ConfigureObjectStorage(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));
            builder.Entity<ObjectData>(b =>
            {
                b.ToTable("ObjectData");
                b.HasKey(o => o.Id);
                b.ConfigureAuditedAggregateRoot();
                b.ConfigureByConvention();
            });
        }
    }
}