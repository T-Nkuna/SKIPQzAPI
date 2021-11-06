using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKIPQzAPI.Integration.PayGate.Models
{
    public class PayGateOption
    {
      public string RETURN_URL { get; set; }
      public long PAYGATE_ID{get; set;}
      public string Encryption_KEY { get; set; }
       public string TRANSACT_URL { get; set; }
    }
}
