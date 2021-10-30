using SKIPQzAPI.Models.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SKIPQzAPI.Models
{
    public class Service :BaseEntity
    {
     
        [MaxLength(256)]
        public string Name { get; set; }

        public string Description { get; set; }
        public double Duration { get; set; }
        public decimal Cost { get; set; }
        public string ImageUrl { get; set; } = "";
    }
}
