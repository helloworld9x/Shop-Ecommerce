using Nop.Core.Domain.Orders;

namespace Nop.Data.Mapping.Orders
{
    public class CheckoutAttributeValueMap : GoqEntityTypeConfiguration<CheckoutAttributeValue>
    {
        public CheckoutAttributeValueMap()
        {
            ToTable("CheckoutAttributeValue");
            HasKey(cav => cav.Id);
            Property(cav => cav.Name).IsRequired().HasMaxLength(400);
            Property(cav => cav.ColorSquaresRgb).HasMaxLength(100);
            Property(cav => cav.PriceAdjustment).HasPrecision(18, 4);
            Property(cav => cav.WeightAdjustment).HasPrecision(18, 4);

            HasRequired(cav => cav.CheckoutAttribute)
                .WithMany(ca => ca.CheckoutAttributeValues)
                .HasForeignKey(cav => cav.CheckoutAttributeId);
        }
    }
}