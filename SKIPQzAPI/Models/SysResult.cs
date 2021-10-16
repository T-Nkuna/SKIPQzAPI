using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKIPQzAPI.Models
{
    public class SysResult<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public bool Ok { get; set; }
    }
}
