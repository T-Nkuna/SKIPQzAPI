using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKIPQzAPI.Models
{
    public class ClientInfo
    {
        public long? ClientInfoId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string IdenitityNumber { get; set; }

        public IdentityUser User { get; set; }

    }
}
