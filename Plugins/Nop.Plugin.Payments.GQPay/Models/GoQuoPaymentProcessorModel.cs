using Nop.Web.Framework.Mvc;

namespace Nop.Plugin.Payments.GQPay.Models
{
    public class GoQuoPaymentProcessorModel : BaseNopEntityModel
    {
        public string Name { get; set; }

        public string Currency { get; set; }

        public bool Active { get; set; }
    }
}
