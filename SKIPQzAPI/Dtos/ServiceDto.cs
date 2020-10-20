using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKIPQzAPI.Dtos
{
    public class ServiceDto
    {
        public string Name { get; set; }


        public double Duration { get; set; }

        public int ServiceId { get; set; }

        public decimal Cost { get; set; }

        public IFormFile ImageFile { get; set; }

        public string ImageUrl { get; set; } = "";
    }
}
