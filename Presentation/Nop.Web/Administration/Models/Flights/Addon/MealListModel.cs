using System.Web.Mvc;
using Nop.Web.Framework;

namespace Nop.Admin.Models.Flights.Addon
{
    public class MealListModel
    {
        [NopResourceDisplayName("Commercial Name")]
        [AllowHtml]
        public string SearchCommercialName { get; set; }

    }
}