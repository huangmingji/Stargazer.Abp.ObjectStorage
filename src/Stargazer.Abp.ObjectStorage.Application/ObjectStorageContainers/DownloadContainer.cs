using Stargazer.Abp.ObjectStorage.Application.Contracts.ObjectStorage;
using Volo.Abp.BlobStoring;

namespace Stargazer.Abp.ObjectStorage.Application.ObjectStorageContainers
{
    [BlobContainerName(ObjectStorageContainerConsts.Download)]
    public abstract class DownloadContainer
    {

    }
}