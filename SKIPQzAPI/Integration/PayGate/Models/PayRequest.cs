using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SKIPQzAPI.Integration.PayGate.Models
{
    public class PayRequest
    {
        public long PAYGATE_ID { get; set; }

        public string REFERENCE { get; set; }

        public decimal AMOUNT { get; set; } = 0m;

        public string CURRENCY { get; set; } = "ZAR";

        public string RETURN_URL { get; set; }

        public string TRANSACTION_DATE { get; set; } =  DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss");

        public string LOCALE { get; set; } = "en-za";

        public string COUNTRY { get; set; } = "ZAF";

        public string EMAIL { get; set; }

        public string CHECKSUM { get; set; }

        public IEnumerable<KeyValuePair<string, string>> AsEnumerable()
        {
            var amountPattern = new Regex(@"\d+");
            var type = typeof(PayRequest);
            return type.GetProperties().Where(propInfo => propInfo.SetMethod.IsPublic && propInfo.DeclaringType == type)
                .Select(propInfo => new KeyValuePair<string, string>(propInfo.Name,propInfo.Name!="AMOUNT"? propInfo.GetValue(this)?.ToString()??"":$"{amountPattern.Match(propInfo.GetValue(this)?.ToString()).Value}"));
        }
    }

}
