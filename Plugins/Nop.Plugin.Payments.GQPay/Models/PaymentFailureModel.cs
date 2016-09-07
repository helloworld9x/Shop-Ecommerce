using System.Collections.Generic;

namespace Nop.Plugin.Payments.GQPay.Models
{
    public class PaymentFailureModel
    {
        public PaymentFailureModel()
        {
            Errors = new List<string>();
        }

        public IList<string> Errors { get; set; }
    }
}
