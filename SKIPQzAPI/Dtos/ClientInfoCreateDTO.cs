using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKIPQzAPI.Dtos
{
    public class ClientInfoCreateDTO: ClientInfoDTO
    {
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
