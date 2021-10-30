using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SKIPQzAPI.Models.Time;
using SKIPQzAPI.Models.Shared;

namespace SKIPQzAPI.Models
{
    public class ServiceProvider: BaseEntity
    {
  
        public IdentityUser User { get; set; }

        [MaxLength(255)]
        public string ImageUrl { get; set; }

        public string Name { get; set; }
        public List<WorkingDay> WorkingDays { get; set; }
  
    }
}
