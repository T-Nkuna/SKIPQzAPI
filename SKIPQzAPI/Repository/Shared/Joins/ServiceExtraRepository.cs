using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SKIPQzAPI.Models;


namespace SKIPQzAPI.Repository.Shared.Joins
{
    public class ServiceExtraRepository : GenericRepository<ServiceExtras>
    {
        public ServiceExtraRepository(IdentityDbContext dbContext) : base(dbContext) { }
    }
}
