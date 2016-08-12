using Nop.Core.Domain.Shipping;

namespace Nop.Data.Mapping.Shipping
{
    public class DeliveryDateMap : GoqEntityTypeConfiguration<DeliveryDate>
    {
        public DeliveryDateMap()
        {
            this.ToTable("DeliveryDate");
            this.HasKey(dd => dd.Id);
            this.Property(dd => dd.Name).IsRequired().HasMaxLength(400);
        }
    }
}
