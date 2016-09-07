using AutoMapper;
using Nop.Plugin.Payments.GQPay.Domain;
using Nop.Plugin.Payments.GQPay.Models;

namespace Nop.Plugin.Payments.GQPay.Extensions
{
    public static class GoQuoMappingExtensions
    {
        public static TDestination MapTo<TSource, TDestination>(this TSource source)
        {
            return Mapper.Map<TSource, TDestination>(source);
        }

        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return Mapper.Map(source, destination);
        }


        public static GoQuoPaymentProcessorModel ToModel(this GoQuoPayProcessor entity)
        {
            return entity.MapTo<GoQuoPayProcessor, GoQuoPaymentProcessorModel>();
        }

        public static GoQuoPayProcessor ToEntity(this GoQuoPaymentProcessorModel model)
        {
            return model.MapTo<GoQuoPaymentProcessorModel, GoQuoPayProcessor>();
        }

        public static GoQuoPayProcessor ToEntity(this GoQuoPaymentProcessorModel model, GoQuoPayProcessor destination)
        {
            return model.MapTo(destination);
        }

    }
}
