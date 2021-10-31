using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SKIPQzAPI.Models;
using SKIPQzAPI.Repository.Shared;


namespace SKIPQzAPI.Repository
{
    public class ServiceProviderRepository : GenericRepository<ServiceProvider>
    {
        public ServiceProviderRepository(IdentityDbContext dbContext) : base(dbContext) { }
    }
}
