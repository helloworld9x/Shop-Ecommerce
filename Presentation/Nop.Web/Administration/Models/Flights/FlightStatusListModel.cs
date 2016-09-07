using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;

namespace Nop.Admin.Models.Flights
{
    public class FlightStatusListModel : BaseNopModel
    {
        [NopResourceDisplayName("Commercial Name")]
        [AllowHtml]
        public string SearchCommercialName { get; set; }

        [NopResourceDisplayName("Sector From")]
        [AllowHtml]
        public string SearchFrom { get; set; }

        [NopResourceDisplayName("Sector To")]
        [AllowHtml]
        public string SearchTo { get; set; }
    }
}