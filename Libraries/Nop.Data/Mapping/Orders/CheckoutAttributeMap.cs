using Nop.Core.Domain.Orders;

namespace Nop.Data.Mapping.Orders
{
    public class CheckoutAttributeMap : GoqEntityTypeConfiguration<CheckoutAttribute>
    {
        public CheckoutAttributeMap()
        {
            ToTable("CheckoutAttribute");
            HasKey(ca => ca.Id);
            Property(ca => ca.Name).IsRequired().HasMaxLength(400);

            Ignore(ca => ca.AttributeControlType);
        }
    }
}