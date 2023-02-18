using System.Threading.Tasks;
using Stargazer.Abp.ObjectStorage.Application.Contracts.ObjectStorage;
using Stargazer.Abp.ObjectStorage.Application.ObjectStorageContainers;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring;

namespace Stargazer.Abp.ObjectStorage.Application.Services
{
    public class OtherObjectService : ApplicationService, IOtherObjectService
    {
        private readonly IBlobContainer<OtherObjectContainer> _blobContainer;

        public OtherObjectService(IBlobContainer<OtherObjectContainer> blobContainer)
        {
            _blobContainer = blobContainer;
        }

        public async Task SaveAsync(string name, byte[] bytes)
        {
            if (await _blobContainer.ExistsAsync(name))
            {
                return;
            }
            await _blobContainer.SaveAsync(name, bytes);
        }

        public async Task<byte[]> GetAsync(string name)
        {
            return await _blobContainer.GetAllBytesOrNullAsync(name);
        }

        public async Task<bool> DeleteAsync(string name)
        {
            return await _blobContainer.DeleteAsync(name);
        }
    }
}