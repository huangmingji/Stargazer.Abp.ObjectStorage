using System.Threading.Tasks;

namespace Stargazer.Abp.ObjectStorage.Application.Contracts.ObjectStorage
{
    public interface IOtherPictureService
    {
        Task SaveAsync(string name, byte[] bytes);

        Task<byte[]> GetAsync(string name);

        Task<bool> DeleteAsync(string name);
    }
}