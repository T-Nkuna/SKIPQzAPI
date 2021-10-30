using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKIPQzAPI.Dtos
{
    public class ExtraDto
    {
        public long? ExtraId { get; set; }

        public decimal Cost { get; set; }

        public string Name { get; set; }

        public double Duration { get; set; }
    }
}
