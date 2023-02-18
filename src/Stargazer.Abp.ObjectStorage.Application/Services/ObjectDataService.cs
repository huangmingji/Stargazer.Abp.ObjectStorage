using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lemon.Common.Extend;
using Microsoft.Extensions.Caching.Distributed;
using Minio.DataModel;
using Stargazer.Abp.ObjectStorage.Application.Contracts.ObjectStorage;
using Stargazer.Abp.ObjectStorage.Application.Contracts.ObjectStorage.Dtos;
using Stargazer.Abp.ObjectStorage.Domain;
using Stargazer.Abp.ObjectStorage.Domain.Repository;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Stargazer.Abp.ObjectStorage.Application.Services
{
    public class ObjectDataService : ApplicationService, IObjectDataService
    {
        private readonly IObjectDataRepository _repository;
        private IDistributedCache<ObjectData> _cache;
        public ObjectDataService(IObjectDataRepository repository, IDistributedCache<ObjectData> cache)
        {
            _repository = repository;
            _cache = cache;
        }

        private const string _objectDataCacheIdKey = "ObjectData::Id:{key}";
        private const string _objectDataCacheHashKey = "ObjectData::Hash:{key}";

        public async Task DeleteAsync(Guid id)
        {
            var data = await _repository.GetAsync(x => x.Id == id);
            await _repository.DeleteAsync(x => x.Id == id);
            await RemoveCacheAsync(data);
        }

        public async Task<ObjectDataDto> GetAsync(Guid id)
        {
            var data = await _cache.GetAsync(string.Format(_objectDataCacheIdKey, id.ToString()));
            if (data == null)
            {
                data = await _repository.GetAsync(x => x.Id == id);
                await SetCacheAsync(data);
            }
            return ObjectMapper.Map<ObjectData, ObjectDataDto>(data);
        }

        public async Task<ObjectDataDto> GetAsync(string hash)
        {
            var data = await _cache.GetAsync(string.Format(_objectDataCacheHashKey, hash));
            if (data == null)
            {
                data = await _repository.GetAsync(x => x.ObjectHash == hash);
                await SetCacheAsync(data);
            }
            return ObjectMapper.Map<ObjectData, ObjectDataDto>(data);
        }

        public async Task<bool> IsExist(string hash)
        {
            var fileData = await _repository.FindAsync(x => x.ObjectHash == hash);
            return fileData != null;
        }

        public async Task<ObjectDataDto> CreateAsync(UpdateObjectDataDto data)
        {
            var fileData = await _repository.FindAsync(x => x.ObjectHash == data.FileHash);
            if (fileData != null)
            {
                return ObjectMapper.Map<ObjectData, ObjectDataDto>(fileData);
            }

            fileData = new ObjectData(GuidGenerator.Create(),
                data.FileType, data.FileExtension, data.FileHash, data.FilePath, data.FileSize);
            var result = await _repository.InsertAsync(fileData);
            return ObjectMapper.Map<ObjectData, ObjectDataDto>(result);
        }

        public async Task<PagedResultDto<ObjectDataDto>> GetListAsync(string fileType, int pageIndex, int pageSize)
        {
            var expression = Expressionable.Create<ObjectData>();
            expression = Expressionable.And(expression, x => x.ObjectType == fileType);
            int total = await _repository.CountAsync(expression);
            var data = await _repository.GetListByPageAsync(expression, pageIndex, pageSize);

            return new PagedResultDto<ObjectDataDto>()
            {
                TotalCount = total,
                Items = ObjectMapper.Map<List<ObjectData>, List<ObjectDataDto>>(data)
            };
        }

        private async Task SetCacheAsync(ObjectData data)
        {
            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(15)
            };
            await _cache.SetAsync(string.Format(_objectDataCacheHashKey, data.ObjectHash), data, options);
            await _cache.SetAsync(string.Format(_objectDataCacheIdKey, data.Id.ToString()), data, options);
        }

        private async Task RemoveCacheAsync(ObjectData data)
        {
            await _cache.RemoveAsync(string.Format(_objectDataCacheHashKey, data.ObjectHash));
            await _cache.RemoveAsync(string.Format(_objectDataCacheIdKey, data.Id.ToString()));
        }
    }
}