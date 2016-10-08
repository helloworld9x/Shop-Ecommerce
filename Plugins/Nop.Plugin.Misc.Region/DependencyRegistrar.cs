using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using Nop.Core.Configuration;
using Nop.Core.Data;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Data;
using Nop.Plugin.Misc.Region.Data;
using Nop.Plugin.Misc.Region.Domain;
using Nop.Plugin.Misc.Region.Services;
using Nop.Services.Catalog;
using Nop.Web.Framework.Mvc;

namespace Nop.Plugin.Misc.Region
{
  public class DependencyRegistrar : IDependencyRegistrar
    {
        private const string CONTEXT_NAME = "nop_object_context_product_view_ProductRegion";
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
      {
            builder.RegisterType<MiscRegionService>().As<IMiscRegionService>().InstancePerLifetimeScope();
            builder.RegisterType<ProductOverrideService>().As<IProductService>().InstancePerLifetimeScope();

            //data context
            this.RegisterPluginDataContext<MiscRegionObjectContext>(builder , CONTEXT_NAME);

            //override required repository with our custom context
            builder.RegisterType<EfRepository<ProductRegion>>()
                .As<IRepository<ProductRegion>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>(CONTEXT_NAME))
                .InstancePerLifetimeScope();
        }

      public int Order { get; }
    }
}
