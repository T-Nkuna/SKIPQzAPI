using SKIPQzAPI.Models.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SKIPQzAPI.Models
{
    public class ServiceProviderServices : BaseEntity
    {
        public Service Service { get; set; }
        public ServiceProvider ServiceProvider { get; set; }
       
    }
}
