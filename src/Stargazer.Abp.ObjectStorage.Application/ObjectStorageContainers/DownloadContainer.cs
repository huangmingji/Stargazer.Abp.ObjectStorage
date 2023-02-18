using Stargazer.Abp.ObjectStorage.Domain.Shared.ObjectStorage;
using Volo.Abp.BlobStoring;

namespace Stargazer.Abp.ObjectStorage.Application.ObjectStorageContainers
{
    [BlobContainerName(ObjectStorageContainerConsts.Download)]
    public abstract class DownloadContainer
    {

    }
}