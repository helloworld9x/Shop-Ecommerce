using System.Collections.Generic;
using Nop.Core.Domain.Security;
using Nop.Core.Domain.Seo;
using Nop.Core.Domain.Stores;

namespace Nop.Core.Domain.Flights
{
    public class FlightStatus : BaseEntity, ISlugSupported, IAclSupported, IStoreMappingSupported
    {
        private ICollection<FlightProduct> _flightProducts;

        public string CommercialName { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public string AirlineCode { get; set; }

        public string AirlineName { get; set; }

        public string FlightNumber { get; set; }

        public string Currency { get; set; }

        public bool Active { get; set; }

        public bool Deleted { get; set; }

        public bool SubjectToAcl { get; set; }

        public bool LimitedToStores { get; set; }

        /// <summary>
        /// Gets or sets the Flight Products
        /// </summary>
        public virtual ICollection<FlightProduct> FlightProducts
        {
            get { return _flightProducts ?? (_flightProducts = new List<FlightProduct>()); }
            protected set { _flightProducts = value; }
        }
    }
}
