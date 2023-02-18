using System;
using System.Threading.Tasks;

namespace Stargazer.Abp.ObjectStorage.Application.Contracts.ObjectStorage
{
    public interface IProfilePictureService
    {
        Task SaveAsync(string name, byte[] bytes);

        Task<byte[]> GetAsync(string name);
    }
}