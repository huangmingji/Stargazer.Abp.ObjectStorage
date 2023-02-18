using Stargazer.Abp.ObjectStorage.Application;
using Stargazer.Abp.ObjectStorage.Application.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace Stargazer.Abp.ObjectStorage.HttpApi.Client
{
    [DependsOn(
        typeof(StargazerAbpObjectStorageApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class StargazerAbpObjectStorageHttpApiClientModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(StargazerAbpObjectStorageApplicationContractsModule).Assembly,
                "oos");
        }
    }
}
