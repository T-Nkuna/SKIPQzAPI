using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKIPQzAPI.Dtos
{
    public class CancelBookingRequestDto
    {
        public string UserName { get; set; }

        public int BookingId { get; set; }
    }
}
