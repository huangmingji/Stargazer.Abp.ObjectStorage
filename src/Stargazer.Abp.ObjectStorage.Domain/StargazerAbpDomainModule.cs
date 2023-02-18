using Stargazer.Abp.ObjectStorage.Domain.Shared;
using Stargazer.Abp.ObjectStorage.Domain.Shared.MultiTenancy;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;

namespace Stargazer.Abp.ObjectStorage.Domain
{
    [DependsOn(
        typeof(StargazerAbpObjectStorageDomainSharedModule),
        typeof(AbpDddDomainModule))]
    public class StargazerAbpObjectStorageDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpMultiTenancyOptions>(options =>
            {
                options.IsEnabled = MultiTenancyConsts.IsEnabled;
            });
        }
    }
}