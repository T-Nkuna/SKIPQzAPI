using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SKIPQzAPI.Models;

namespace SKIPQzAPI.Repository.Shared
{
    public class ExtraRepository:GenericRepository<Extra>
    {
        public ExtraRepository(IdentityDbContext dbContext) : base(dbContext) { }
    }
}
