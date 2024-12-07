using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Application.Dto.Payment
{
    public class VNPayCallbackDTO
    {
        public string TransactionId { get; set; }
        public string ResponseCode { get; set; }
        public decimal Amount { get; set; }
        public string Signature { get; set; }
        public string BankCode { get; set; }
        public string Description { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string RawData { get; set; }
    }
}
