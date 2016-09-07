using Nop.Web.Framework.Mvc;

namespace Nop.Plugin.Payments.GQPay.Models
{
    public class ProcessorKeyValueModel : BaseNopEntityModel
    {
        public string ProcessKey { get; set; }

        public string ProcessValue { get; set; }

        public int ProcessId { get; set; }

        public string ProcessName { get; set; }
    }
}
