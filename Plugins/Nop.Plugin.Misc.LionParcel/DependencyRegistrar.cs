//using Autofac;
//using Autofac.Core;
//using Nop.Core.Configuration;
//using Nop.Core.Data;
//using Nop.Core.Infrastructure;
//using Nop.Core.Infrastructure.DependencyManagement;
//using Nop.Data;
//using Nop.Plugin.Misc.LionParcel.Services;
//using Nop.Services.Catalog;
//using Nop.Web.Framework.Mvc;

//namespace Nop.Plugin.Misc.LionParcel
//{
//    public class DependencyRegistrar : IDependencyRegistrar
//    {
//        private const string CONTEXT_NAME = "nop_object_context_product_view_ProductRegion";
//        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
//        {
//            builder.RegisterType<MiscRegionService>().As<IMiscRegionService>().InstancePerLifetimeScope();
//            builder.RegisterType<ProductOverrideService>().As<IProductService>().InstancePerLifetimeScope();

//        }

//        public int Order { get; }
//    }
//}
