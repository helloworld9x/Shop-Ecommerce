using System.Collections.Generic;
using Nop.Core;

namespace Nop.Plugin.Payments.GQPay.Domain
{
    public class GoQuoPayProcessor : BaseEntity
    {
        private ICollection<ProcessorKeyValue> _processorKeyValues;

        public virtual string Name { get; set; }

        public virtual string Currency { get; set; }

        public virtual bool Active { get; set; }

        /// <summary>
        /// Gets or sets locale string resources
        /// </summary>
        public virtual ICollection<ProcessorKeyValue> ProcessorKeyValues
        {
            get { return _processorKeyValues ?? (_processorKeyValues = new List<ProcessorKeyValue>()); }
            protected set { _processorKeyValues = value; }
        }

    }
}
