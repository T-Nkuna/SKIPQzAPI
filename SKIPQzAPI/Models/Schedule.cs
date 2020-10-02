using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SKIPQzAPI.Models
{
    public class Schedule
    {
        [MaxLength(255)]
        public string Name { get; set; }
        public int ScheduleId { get; set; }
    }
}
