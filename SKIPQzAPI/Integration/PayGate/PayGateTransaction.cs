using System.Linq;
using SKIPQzAPI.Integration.PayGate.Models;
using System.Security.Cryptography;
using System.Text;

namespace SKIPQzAPI.Integration.PayGate
{
    public class Payment
    {
        private readonly PayRequest _request;
       
        
        public Payment(decimal amount,string reference, string notifyEmail,PayGateOption payGateOption)
        {
            _request = new PayRequest { 
                AMOUNT = amount * 100,
                EMAIL = notifyEmail, 
                REFERENCE = reference,
                RETURN_URL = payGateOption.RETURN_URL,
                PAYGATE_ID = payGateOption.PAYGATE_ID,
            };

            var checkSumStr = ValueString(_request) + payGateOption.Encryption_KEY;
           // _request.CHECKSUM = MD5(checkSumStr);


        }

        public PayRequest PaymentRequest => _request;

        public string ValueString(PayRequest request)
        {
            var type = request.GetType();
            return type.GetProperties()
                .Where(prop=>prop.CanRead && prop.CanWrite)
                .Select(prop =>
                {
                    return prop.GetValue(request)?.ToString()??"";
                }).Aggregate("", (carry, next) => carry + next);
        }
    }
}