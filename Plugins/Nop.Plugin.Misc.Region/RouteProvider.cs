using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using Nop.Web.Framework.Mvc.Routes;

namespace Nop.Plugin.Misc.Region
{
   public class RouteProvider : IRouteProvider
    {
       public void RegisterRoutes(RouteCollection routes)
       {
            routes.MapRoute("Plugin.Misc.Region.Configure",
                  "Plugins/Region/Configure",
                  new { controller = "MiscRegion", action = "Configure" },
                  new[] { "Nop.Plugin.Misc.MiscRegion.Controllers" }
             );
            routes.MapRoute("Plugin.Misc.Region.ProductRestriction",
                  "MiscRegion/ProductRestriction/{Id}",
                  new { controller = "MiscRegion", action = "ProductRestriction", Id = "" },
                  new[] { "Nop.Plugin.Misc.MiscRegion.Controllers" }
             );
        }

       public int Priority { get; }
    }
}
