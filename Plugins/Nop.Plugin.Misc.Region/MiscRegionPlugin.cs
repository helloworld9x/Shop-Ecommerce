using System.Web.Routing;
using Nop.Core.Plugins;
using Nop.Plugin.Misc.Region.Data;
using Nop.Services.Common;
using Nop.Services.Localization;

namespace Nop.Plugin.Misc.Region
{
    class MiscRegionPlugin : BasePlugin,IMiscPlugin
    {
        private readonly MiscRegionObjectContext _context;
        public MiscRegionPlugin (MiscRegionObjectContext context)
        {
            _context = context;
        }

        public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "Configure";
            controllerName = "MiscRegion";
            routeValues = new RouteValueDictionary { { "Namespaces", "Nop.Plugin.Misc.Region.Controllers" }, { "area", null } };
        }

        public override void Install()
        {
            this.AddOrUpdatePluginLocaleResource("Nop.Plugin.Misc.Region.Fields.Restriction", "Restriction");
            this.AddOrUpdatePluginLocaleResource("Nop.Plugin.Misc.Region.Fields.ProductRestriction", "Product Restriction");
            this.AddOrUpdatePluginLocaleResource("Admin.Configuration.product.Restrictions.Description", "Please mark the checkbox(es) for the country or countries in which you want the Product(s)  not available ");
            this.AddOrUpdatePluginLocaleResource("admin.configuration.product.restrictions.country", "Country");
            this.AddOrUpdatePluginLocaleResource("Admin.Configuration.product.Restrictions", "product Restrictions");
            this.AddOrUpdatePluginLocaleResource("Admin.Configuration.product.Restrictions.Updated", "Product Restriction Updated Success");
            this.AddOrUpdatePluginLocaleResource("", "");
            _context.Install();
            base.Install();
        }

        public override void Uninstall()
        {
            this.DeletePluginLocaleResource("Nop.Plugin.Misc.Region.Fields.Restriction");
            this.DeletePluginLocaleResource("Nop.Plugin.Misc.Region.Fields.ProductRestriction");
            this.DeletePluginLocaleResource("");
            _context.Uninstall();
            base.Uninstall();
        }
    }
}
