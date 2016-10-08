using System.Web.Mvc;
using System.Web.Routing;
using Nop.Web.Framework.Mvc.Routes;

namespace Nop.Plugin.Misc.LionParcel
{
   public class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {

            //We reordered our routes so the most used ones are on top. It can improve performance.
            routes.MapRoute("TariffSearch",
                         "tariff-checking/",
                         new { controller = "LionParcel", action = "TariffSearch" },
                         new[] { "Nop.Plugin.Misc.LionParcel.Controllers" });

            routes.MapRoute("TrackingSearch",
                         "tracking-checking/",
                         new { controller = "LionParcel", action = "TrackingSearch" },
                         new[] { "Nop.Plugin.Misc.LionParcel.Controllers" });

            routes.MapRoute("PosRegistration",
                       "pos-registration/",
                       new { controller = "LionParcel", action = "PosRegistration" },
                       new[] { "Nop.Plugin.Misc.LionParcel.Controllers" });

            routes.MapRoute("PingGetRequestHttpClient",
                       "ping-get-request/",
                       new { controller = "LionParcel", action = "PingGetRequestHttpClient" },
                       new[] { "Nop.Plugin.Misc.LionParcel.Controllers" });

            routes.MapRoute("Network",
                      "network/",
                      new { controller = "LionParcel", action = "Network" },
                      new[] { "Nop.Plugin.Misc.LionParcel.Controllers" });

            routes.MapRoute("Address",
                     "network-address-insert/",
                     new { controller = "LionParcel", action = "Address" },
                     new[] { "Nop.Plugin.Misc.LionParcel.Controllers" });

            routes.MapRoute("GetAddressLocation",
                     "get-locations/",
                     new { controller = "LionParcel", action = "GetAddressLocation" },
                     new[] { "Nop.Plugin.Misc.LionParcel.Controllers" });

            routes.MapRoute("Plugin.Misc.LionParcel.Index",
                 "shop/",
                 new { controller = "LionParcel", action = "Index" },
                 new[] { "Nop.Plugin.Misc.LionParcel.Controllers" }
            );
        }

        public int Priority { get; }
    }
}
