using Stargazer.Abp.ObjectStorage.Application;
using Stargazer.Abp.ObjectStorage.EntityFrameworkCore.DbMigrations;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Stargazer.Abp.ObjectStorage.DbMigrations
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(StargazerAbpObjectStorageEntityFrameworkCoreDbMigrationsModule),
        typeof(StargazerAbpObjectStorageApplicationModule)
    )]
    public class StargazerAbpObjectStorageDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            // Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}