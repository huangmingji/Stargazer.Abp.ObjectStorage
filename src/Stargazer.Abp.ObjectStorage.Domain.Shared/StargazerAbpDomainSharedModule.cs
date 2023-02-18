using Stargazer.Abp.ObjectStorage.Domain.Shared.MultiTenancy;
using Volo.Abp.Modularity;
using Volo.Abp.Validation;

namespace Stargazer.Abp.ObjectStorage.Domain.Shared
{
    [DependsOn(
        typeof(AbpValidationModule))]
    public class StargazerAbpObjectStorageDomainSharedModule : AbpModule
    {
        
    }
}
