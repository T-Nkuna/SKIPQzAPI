using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SKIPQzAPI.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SKIPQzAPI.Repository.Shared
{

    public class GenericRepository<T>:IRepository<T> where T:BaseEntity
    {
        private readonly IdentityDbContext _dbContext;
        private readonly DbSet<T> _set;
        public GenericRepository(IdentityDbContext dbConext)
        {
            _dbContext = dbConext;
            _set = _dbContext.Set<T>();
        }

        public virtual int Add(T entity, long? organisationId, string userId)
        {
            entity.OrganisationId = organisationId;
            entity.CreatedOn = DateTime.Now;
            entity.CreatedBy = userId;
            _set.Add(entity);
            return _dbContext.SaveChanges();
        }

        public virtual IQueryable<T> Get(Expression<Func<T, bool>> filter) => _set.AsNoTracking().Where(filter);
       

        public virtual int Remove(T entity, string userId)
        {
           
            entity.Inactive = true;
            return this.Update(entity,userId);
        }

        public virtual int Update(T entity, string userId)
        {
            entity.ModifiedOn = DateTime.Now;
            entity.ModifiedBy = userId;
            _set.Update(entity);
            return _dbContext.SaveChanges();
        }
    }
}
