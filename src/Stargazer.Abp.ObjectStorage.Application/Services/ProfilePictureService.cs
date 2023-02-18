using System;
using System.Threading.Tasks;
using Stargazer.Abp.ObjectStorage.Application.Contracts.ObjectStorage;
using Stargazer.Abp.ObjectStorage.Application.ObjectStorageContainers;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring;

namespace Stargazer.Abp.ObjectStorage.Application.Services
{
    public class ProfilePictureService : ApplicationService, IProfilePictureService
    {
        private readonly IBlobContainer<ProfilePictureContainer> _blobContainer;

        public ProfilePictureService(IBlobContainer<ProfilePictureContainer> blobContainer)
        {
            _blobContainer = blobContainer;
        }
        public async Task SaveAsync(string name, byte[] bytes)
        {
            await _blobContainer.SaveAsync(name, bytes, overrideExisting: true);
        }

        public async Task<byte[]> GetAsync(string name)
        {
            return await _blobContainer.GetAllBytesOrNullAsync(name);
        }
    }
}