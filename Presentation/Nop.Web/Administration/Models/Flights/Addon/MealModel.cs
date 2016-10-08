using Nop.Web.Framework.Mvc;

namespace Nop.Admin.Models.Flights.Addon
{
    public class MealModel : FlightProductModel
    {
        public MealAttributes MealAttributes { get; set; }
    }
    public class AppliedToProductModel : BaseNopModel
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }
    }
    public class MealAttributes
    {
        public string MealDesc { get; set; }

        public string MealShortDesc { get; set; }

        public string ApplicableToClass { get; set; }

        public decimal EconomyPrice { get; set; }

        public decimal BusinessPrice { get; set; }

        public string Group { get; set; }
    }
}