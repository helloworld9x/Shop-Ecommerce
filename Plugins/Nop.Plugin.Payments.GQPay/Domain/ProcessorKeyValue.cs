using Nop.Core;

namespace Nop.Plugin.Payments.GQPay.Domain
{
    public class ProcessorKeyValue : BaseEntity
    {
        public virtual string ProcessKey { get; set; }

        public virtual string ProcessValue { get; set; }

        public virtual int ProcessId { get; set; }

        public virtual GoQuoPayProcessor GoQuoPayProcessors { get; set; }

    }
}
