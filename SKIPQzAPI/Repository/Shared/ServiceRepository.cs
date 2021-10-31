using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SKIPQzAPI.Models;


namespace SKIPQzAPI.Repository.Shared
{
    public class ServiceRepository:GenericRepository<Service>
    {
        public ServiceRepository(IdentityDbContext dbContext) : base(dbContext) { }
    }
}
