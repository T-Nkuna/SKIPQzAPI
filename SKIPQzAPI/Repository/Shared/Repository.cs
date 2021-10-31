using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SKIPQzAPI.Models.Shared;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SKIPQzAPI.Repository.Shared
{
    public interface IRepository<TEntity> where TEntity:BaseEntity
    {
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter);

        int Add(TEntity entity,long? organisationId,string userId);

        int Remove(TEntity entity,string userId);

        int Update(TEntity entity,string userId);
    }
}