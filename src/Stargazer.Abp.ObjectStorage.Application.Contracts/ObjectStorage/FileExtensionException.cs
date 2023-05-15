using Volo.Abp;

namespace Stargazer.Abp.ObjectStorage.Application.Contracts.ObjectStorage.Dtos
{
    public class FileExtensionException : BusinessException
    {
        public FileExtensionException(string message) : base(
            message: message)
        {
        }
    }
}