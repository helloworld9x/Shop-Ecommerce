using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Security;
using Nop.Core.Domain.Seo;
using Nop.Core.Domain.Stores;

namespace Nop.Core.Domain.Flights
{
    public class FlightProduct : BaseEntity, ISlugSupported, IAclSupported, IStoreMappingSupported
    {
        private ICollection<FlightStatus> _flightstatus;

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Currency { get; set; }

        public decimal Price { get; set; }

        public string Attributes { get; set; }

        public FlightProductType Type { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the customer is active
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the flight has been deleted
        /// </summary>
        public bool Deleted { get; set; }

        public bool SubjectToAcl { get; set; }

        public bool LimitedToStores { get; set; }


        /// <summary>
        /// Gets or sets the Flight Products
        /// </summary>
        public virtual ICollection<FlightStatus> FlightStatus
        {
            get { return _flightstatus ?? (_flightstatus = new List<FlightStatus>()); }
            protected set { _flightstatus = value; }
        }

        public virtual Product Product { get; set; }
    }

    public enum FlightProductType : byte
    {
        Meal,
        Seat,
        Baggage,
        Other
    }
}
