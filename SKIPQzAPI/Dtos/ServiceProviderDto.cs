using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SKIPQzAPI.Dtos
{
    public class ServiceProviderDto
    {
        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Email { get; set; }

        public int ServiceProviderId { get; set; }

        public List<WorkingDayDto> ScheduledWorkDays { get; set; }

        public List<ServiceDto> Services { get; set; }

        public string ImageUrl { get; set; }

        public IFormFile ImageFile { get; set; }
    }

}
