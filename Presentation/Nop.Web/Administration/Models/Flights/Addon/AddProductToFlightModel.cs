using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;

namespace Nop.Admin.Models.Flights.Addon
{
    public class AddProductToFlightModel : BaseNopModel
    {

        [NopResourceDisplayName("Search Commercial Name")]
        [AllowHtml]
        public string SearchCommercialName { get; set; }

        [NopResourceDisplayName("Search Flight Code")]
        [AllowHtml]
        public string SearchFlightCode { get; set; }

        [NopResourceDisplayName("From")]
        [AllowHtml]
        public string From { get; set; }

        [NopResourceDisplayName("To")]
        [AllowHtml]
        public string To { get; set; }

        public int FlightStatusId { get; set; }

        public int[] SelectedProductIds { get; set; }
    }
}