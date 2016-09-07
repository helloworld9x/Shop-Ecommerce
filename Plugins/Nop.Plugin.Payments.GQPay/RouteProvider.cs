using System.Web.Mvc;
using System.Web.Routing;
using Nop.Web.Framework.Mvc.Routes;

namespace Nop.Plugin.Payments.GQPay
{
   public class RouteProvider : IRouteProvider
    {
       public void RegisterRoutes(RouteCollection routes)
       {
            routes.MapRoute("Plugin.Payments.GQPay.Configure",
                 "Plugins/GQPay/Configure",
                 new { controller = "GoQuoPaymentGateWays", action = "Configure" },
                 new[] { "Nop.Plugin.Payments.GQPay.Controllers" }
            );
            routes.MapRoute("Plugin.Payments.GQPay.GetAllProcess",
                   "Plugins/GQPay/GetAllProcess",
                   new { controller = "GoQuoPaymentGateWays", action = "GetAllProcess", },
                   new[] { "Nop.Plugin.Payments.GQPay.Controllers" }
              );
            routes.MapRoute("Plugin.Payments.GQPay.UpdateProcess",
                   "Plugins/GQPay/UpdateProcess",
                   new { controller = "GoQuoPaymentGateWays", action = "UpdateProcess", },
                   new[] { "Nop.Plugin.Payments.GQPay.Controllers" }
              );
            routes.MapRoute("Plugin.Payments.GQPay.DeleteProcess",
                   "Plugins/GQPay/DeleteProcess",
                   new { controller = "GoQuoPaymentGateWays", action = "DeleteProcess", },
                   new[] { "Nop.Plugin.Payments.GQPay.Controllers" }
              );
            routes.MapRoute("Plugin.Payments.GQPay.CreateProcess",
                   "Plugins/GQPay/CreateProcess",
                   new { controller = "GoQuoPaymentGateWays", action = "CreateProcess", },
                   new[] { "Nop.Plugin.Payments.GQPay.Controllers" }
              );
            routes.MapRoute("Plugin.Payments.GQPay.ProcessKeyValue",
                   "Plugins/GQPay/ProcessKeyValue",
                   new { controller = "GoQuoPaymentGateWays", action = "ProcessKeyValue", },
                   new[] { "Nop.Plugin.Payments.GQPay.Controllers" }
              );
            routes.MapRoute("Plugin.Payments.GQPay.AddProcessKeyValue",
                   "Plugins/GQPay/AddProcessKeyValue",
                   new { controller = "GoQuoPaymentGateWays", action = "AddProcessKeyValue", },
                   new[] { "Nop.Plugin.Payments.GQPay.Controllers" }
              );
            routes.MapRoute("Plugin.Payments.GQPay.UpdateProcessKeyValue",
                   "Plugins/GQPay/UpdateProcessKeyValue",
                   new { controller = "GoQuoPaymentGateWays", action = "UpdateProcessKeyValue", },
                   new[] { "Nop.Plugin.Payments.GQPay.Controllers" }
              );
            routes.MapRoute("Plugin.Payments.GQPay.DeleteProcessKeyValue",
                   "Plugins/GQPay/DeleteProcessKeyValue",
                   new { controller = "GoQuoPaymentGateWays", action = "DeleteProcessKeyValue", },
                   new[] { "Nop.Plugin.Payments.GQPay.Controllers" }
              );

            //payment result
            routes.MapRoute("Plugin.Payments.GQPay.ResponsesResult",
                 "Plugins/GQPay/ResponsesResult",
                 new { controller = "GoQuoPaymentGateWays", action = "ResponsesResult" },
                 new[] { "Nop.Plugin.Payments.GQPay.Controllers" }
            );

            routes.MapRoute("Plugin.Payments.GQPay.PaymentFailuResult",
              "Plugins/GQPay/PaymentFailuResult",
              new { controller = "GoQuoPaymentGateWays", action = "PaymentFailuResult" },
              new[] { "Nop.Plugin.Payments.GQPay.Controllers" }
         );
        }

       public int Priority => 0;
    }
}
