using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Stargazer.Abp.ObjectStorage.Domain.Repository;

public interface IObjectDataRepository: IRepository<ObjectData, Guid>
{
    Task<List<ObjectData>> GetListByPageAsync(Expression<Func<ObjectData, bool>> expression, int pageIndex, int pageSize);
}