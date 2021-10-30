using Microsoft.AspNetCore.Identity;
using SKIPQzAPI.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKIPQzAPI.Models
{
    public class ClientInfo: BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string IdenitityNumber { get; set; }

        public IdentityUser User { get; set; }

    }
}
