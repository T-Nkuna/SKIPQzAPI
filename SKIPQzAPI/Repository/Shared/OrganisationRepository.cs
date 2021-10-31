using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SKIPQzAPI.Models;


namespace SKIPQzAPI.Repository.Shared
{
    public class OrganisationRepository:GenericRepository<Organisation>
    {
        public OrganisationRepository(IdentityDbContext dbContext):base(dbContext)
        { }
    }
}
