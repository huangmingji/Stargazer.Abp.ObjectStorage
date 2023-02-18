using System;
using System.Threading.Tasks;
using Stargazer.Abp.ObjectStorage.Application.Contracts.ObjectStorage.Dtos;
using Stargazer.Abp.ObjectStorage.Domain;
using Volo.Abp.Application.Dtos;

namespace Stargazer.Abp.ObjectStorage.Application.Contracts.ObjectStorage
{
    public interface IObjectDataService
    {
        Task DeleteAsync(Guid id);

        Task<ObjectDataDto> GetAsync(Guid id);

        Task<ObjectDataDto> GetAsync(string hash);

        Task<bool> IsExist(string hash);

        Task<ObjectDataDto> CreateAsync(UpdateObjectDataDto data);
        
        // Task<ObjectDataDto> UpdateAsync(Guid id, UpdateObjectDataDto data);

        Task<PagedResultDto<ObjectDataDto>> GetListAsync(string fileType,int pageIndex, int pageSize);
    }
}