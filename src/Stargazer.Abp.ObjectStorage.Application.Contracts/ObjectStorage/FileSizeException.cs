using Volo.Abp;

namespace Stargazer.Abp.ObjectStorage.Application.Contracts.ObjectStorage.Dtos
{
    public class FileSizeException : BusinessException
    {
        public FileSizeException(string message) : base(
            message: message)
        {
        }
    }
}