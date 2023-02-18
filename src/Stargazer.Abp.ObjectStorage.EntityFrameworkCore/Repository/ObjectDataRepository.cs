using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Stargazer.Abp.ObjectStorage.Domain;
using Stargazer.Abp.ObjectStorage.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Stargazer.Abp.ObjectStorage.EntityFrameworkCore.Repository;

public class ObjectDataRepository :  EfCoreRepository<EntityFrameworkCoreDbContext, ObjectData, Guid>, IObjectDataRepository
{
    public ObjectDataRepository(IDbContextProvider<EntityFrameworkCoreDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task<List<ObjectData>> GetListByPageAsync(Expression<Func<ObjectData, bool>> expression, int pageIndex, int pageSize)
    {
        var queryable = await GetQueryableAsync();
        return await queryable.Where(expression).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
    }
}