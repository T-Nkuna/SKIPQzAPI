using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SKIPQzAPI.Models
{
    public class ServiceProvider
    {
  
        public int ServiceProviderId { get; set; }
        public IdentityUser User { get; set; }
        public Schedule Schedule { get; set; }
   
        [MaxLength(255)]
        public string ImageUrl { get; set; }
  
    }
}
