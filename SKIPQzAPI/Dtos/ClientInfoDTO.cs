using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKIPQzAPI.Dtos
{
    public class ClientInfoDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string IdenitityNumber { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }
    }
}
