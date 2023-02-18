using Volo.Abp.Application;
using Volo.Abp.FluentValidation;
using Volo.Abp.Modularity;

namespace Stargazer.Abp.ObjectStorage.Application.Contracts
{
    [DependsOn(
        typeof(AbpFluentValidationModule),
        typeof(AbpDddApplicationContractsModule)
    )]
    public class StargazerAbpObjectStorageApplicationContractsModule : AbpModule
    {
        
    }
}