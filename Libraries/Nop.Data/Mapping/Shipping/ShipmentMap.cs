using Nop.Core.Domain.Shipping;

namespace Nop.Data.Mapping.Shipping
{
    public partial class ShipmentMap : GoqEntityTypeConfiguration<Shipment>
    {
        public ShipmentMap()
        {
            this.ToTable("Shipment");
            this.HasKey(s => s.Id);

            this.Property(s => s.TotalWeight).HasPrecision(18, 4);
            
            this.HasRequired(s => s.Order)
                .WithMany(o => o.Shipments)
                .HasForeignKey(s => s.OrderId);
        }
    }
}