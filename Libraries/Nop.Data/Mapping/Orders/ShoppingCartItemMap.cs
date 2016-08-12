using Nop.Core.Domain.Orders;

namespace Nop.Data.Mapping.Orders
{
    public class ShoppingCartItemMap : GoqEntityTypeConfiguration<ShoppingCartItem>
    {
        public ShoppingCartItemMap()
        {
            ToTable("ShoppingCartItem");
            HasKey(sci => sci.Id);

            Property(sci => sci.CustomerEnteredPrice).HasPrecision(18, 4);

            Ignore(sci => sci.ShoppingCartType);
            Ignore(sci => sci.IsFreeShipping);
            Ignore(sci => sci.IsShipEnabled);
            Ignore(sci => sci.AdditionalShippingCharge);
            Ignore(sci => sci.IsTaxExempt);

            HasRequired(sci => sci.Customer)
                .WithMany(c => c.ShoppingCartItems)
                .HasForeignKey(sci => sci.CustomerId);

            HasRequired(sci => sci.Product)
                .WithMany()
                .HasForeignKey(sci => sci.ProductId);
        }
    }
}
