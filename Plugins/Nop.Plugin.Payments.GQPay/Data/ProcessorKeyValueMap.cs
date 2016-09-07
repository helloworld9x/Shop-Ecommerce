using System.Data.Entity.ModelConfiguration;
using Nop.Plugin.Payments.GQPay.Domain;

namespace Nop.Plugin.Payments.GQPay.Data
{
   public class ProcessorKeyValueMap : EntityTypeConfiguration<ProcessorKeyValue>
   {
       public ProcessorKeyValueMap()
       {
           ToTable("ProcessorKeyValues");
           HasKey(x => x.Id);
           Property(x => x.ProcessKey);
           Property(x => x.ProcessValue);
           HasRequired(x => x.GoQuoPayProcessors).WithMany(x => x.ProcessorKeyValues).HasForeignKey(x => x.ProcessId);
       }
   }
}
