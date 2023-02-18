using Stargazer.Abp.ObjectStorage.Application;
using Stargazer.Abp.ObjectStorage.Application.Contracts;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace Stargazer.Abp.ObjectStorage.HttpApi
{
    [DependsOn(
        typeof(StargazerAbpObjectStorageApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule)
    )]
    public class StargazerAbpObjectStorageHttpApiModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
        }
    }
}