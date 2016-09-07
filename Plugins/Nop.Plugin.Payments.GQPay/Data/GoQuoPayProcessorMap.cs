using System.Data.Entity.ModelConfiguration;
using Nop.Plugin.Payments.GQPay.Domain;

namespace Nop.Plugin.Payments.GQPay.Data
{
    public class GoQuoPayProcessorMap : EntityTypeConfiguration<GoQuoPayProcessor>
    {
        public GoQuoPayProcessorMap()
        {
            ToTable("GoQuoPayProcessors");
            HasKey(x => x.Id);
            Property(x => x.Name).HasMaxLength(50);
            Property(x => x.Currency).HasMaxLength(255);
            Property(x => x.Active);
        }
    }
}
