using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SKIPQzAPI.Models;


namespace SKIPQzAPI.Repository.Shared.Joins
{
    public class ServiceProviderServiceRepository : GenericRepository<ServiceExtras>
    {
        public ServiceProviderServiceRepository(IdentityDbContext dbContext) : base(dbContext) { }
    }
}
