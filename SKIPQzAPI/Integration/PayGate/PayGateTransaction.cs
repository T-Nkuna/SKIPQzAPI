using System.Linq;
using SKIPQzAPI.Integration.PayGate.Models;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace SKIPQzAPI.Integration.PayGate
{
    public class Payment
    {
        private readonly PayRequest _request;
        private readonly PaygatePayweb3 _paygate;
        
        public Payment(decimal amount,string reference, string notifyEmail,PayGateOption payGateOption)
        {
            _request = new PayRequest { 
                AMOUNT = amount * 100,
                EMAIL = notifyEmail, 
                REFERENCE = reference,
                RETURN_URL = payGateOption.RETURN_URL,
                PAYGATE_ID = payGateOption.PAYGATE_ID,
            };

            _paygate = new PaygatePayweb3();
            _paygate.RequestPayload = _request.AsEnumerable().ToDictionary(k=>k.Key,v=>v.Value);
            _paygate.EncryptionKey = payGateOption.Encryption_KEY;
            _request.CHECKSUM = _paygate.GenerateChecksum(_paygate.RequestPayload);

        }

        public PayRequest PaymentRequest => _request;
        
       public async Task<NameValueCollection> ProcessPaymentRequest() => await _paygate.DoInitiate();
          
        
    }
}