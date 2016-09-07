using Autofac;
using Autofac.Core;
using Nop.Core.Configuration;
using Nop.Core.Data;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Data;
using Nop.Plugin.Payments.GQPay.Data;
using Nop.Plugin.Payments.GQPay.Domain;
using Nop.Plugin.Payments.GQPay.Services;
using Nop.Web.Framework.Mvc;

namespace Nop.Plugin.Payments.GQPay
{
   public class DependencyRegistrar :IDependencyRegistrar
    {
        private const string ContextName = "nop_object_context_process_view_GoQuoPaymentProcessor";

        private const string ContextNameKeyvalue = "nop_object_context_process_view_ProcessorKeyValues";
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
       {

            builder.RegisterType<GoQuoPaymentGateWaysService>().As<IGoQuoPaymentProcessorService>().InstancePerLifetimeScope();

            //data context
            this.RegisterPluginDataContext<GoQuoPayProcessorObjectContext>(builder, ContextName);

            //override required repository with our custom context
            builder.RegisterType<EfRepository<GoQuoPayProcessor>>()
                .As<IRepository<GoQuoPayProcessor>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>(ContextName))
                .InstancePerLifetimeScope();
            
            
             //Register for KeyValue
            builder.RegisterType<ProcessorKeyValueService>().As<IProcessorKeyValueService>().InstancePerLifetimeScope();

            //data context for KeyValue
            this.RegisterPluginDataContext<ProcessorKeyValueObjectContext>(builder, ContextNameKeyvalue);

            //override required repository with our custom context for KeyValue
            builder.RegisterType<EfRepository<ProcessorKeyValue>>()
                .As<IRepository<ProcessorKeyValue>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>(ContextNameKeyvalue))
                .InstancePerLifetimeScope();

        }

       public int Order { get { return 1; } }
    }
}
