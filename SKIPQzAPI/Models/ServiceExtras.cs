using SKIPQzAPI.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKIPQzAPI.Models
{
    public class ServiceExtras : BaseEntity
    {
        public Service Service { get; set; }

        public Extra Extra { get; set; }
    }
}
