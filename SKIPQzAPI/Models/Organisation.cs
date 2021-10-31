using Microsoft.AspNetCore.Identity;
using SKIPQzAPI.Models.Shared;


namespace SKIPQzAPI.Models
{
    public class Organisation: BaseEntity
    {
       public IdentityUser User { get; set; }

       public string Name { get; set; }
    }
}
