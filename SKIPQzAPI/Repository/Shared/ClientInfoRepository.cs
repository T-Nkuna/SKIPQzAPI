using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SKIPQzAPI.Models;

namespace SKIPQzAPI.Repository.Shared
{
    public class ClientInfoRepository: GenericRepository<ClientInfo>
    {
        public ClientInfoRepository(IdentityDbContext dbContext) : base(dbContext) { }
    }
}
