using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKIPQzAPI.Integration.PayGate.Models
{
    public class PayRequest
    {
        public long PAYGATE_ID { get; set; }

        public string REFERENCE { get; set; }

        public decimal AMOUNT { get; set; }

        public string CURRENCY { get; set; } = "ZAR";

        public string RETURN_URL { get; set; }

        public string TRANSACTION_DATE { get; set; } = DateTime.Now.ToString("yyyy-MM-dd H:m:ss");

        public string LOCALE { get; set; } = "en-za";

        public string COUNTRY { get; set; } = "ZAF";

        public string EMAIL { get; set; }

        public string CHECKSUM { get; set; }
    }
}
