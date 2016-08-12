using Nop.Data.Mapping;
using Nop.Plugin.Shipping.ByWeight.Domain;

namespace Nop.Plugin.Shipping.ByWeight.Data
{
    public partial class ShippingByWeightRecordMap : GoqEntityTypeConfiguration<ShippingByWeightRecord>
    {
        public ShippingByWeightRecordMap()
        {
            this.ToTable("ShippingByWeight");
            this.HasKey(x => x.Id);

            this.Property(x => x.Zip).HasMaxLength(400);
        }
    }
}