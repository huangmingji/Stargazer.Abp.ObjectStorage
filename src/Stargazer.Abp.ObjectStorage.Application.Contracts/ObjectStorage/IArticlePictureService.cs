using System.Threading.Tasks;

namespace Stargazer.Abp.ObjectStorage.Application.Contracts.ObjectStorage
{
    public interface IArticlePictureService
    {
        Task SaveAsync(string name, byte[] bytes);

        Task<byte[]> GetAsync(string name);
    }
}