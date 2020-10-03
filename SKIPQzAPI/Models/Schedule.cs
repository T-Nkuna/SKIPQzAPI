using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SKIPQzAPI.Models
{
    public enum ScheduleOwner { Service,ServiceProvider};
    public class Schedule
    {
        [MaxLength(255)]
        public string Name { get; set; }
        public int ScheduleId { get; set; }
        public List<TimeSlot> TimeSlots { get; set; }
        public ScheduleOwner Owner { get; set; }
        public int OwnerId { get; set; }
    }
}
