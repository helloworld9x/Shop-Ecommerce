using System.Web.Routing;
using Nop.Core.Plugins;
using Nop.Services.Common;

namespace Nop.Plugin.Misc.LionParcel
{
    public class LionParcelPlugin : BasePlugin, IMiscPlugin
    {
        public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {

            actionName = "Configure";
            controllerName = "LionParcel";
            routeValues = new RouteValueDictionary { { "Namespaces", "Nop.Plugin.Misc.LionParcel.Controllers" }, { "area", null } };
        }
    }
}
