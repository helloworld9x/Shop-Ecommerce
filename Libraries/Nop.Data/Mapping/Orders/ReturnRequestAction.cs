using Nop.Core.Domain.Orders;

namespace Nop.Data.Mapping.Orders
{
    public class ReturnRequestActionMap : GoqEntityTypeConfiguration<ReturnRequestAction>
    {
        public ReturnRequestActionMap()
        {
            this.ToTable("ReturnRequestAction");
            this.HasKey(rra => rra.Id);
            this.Property(rra => rra.Name).IsRequired().HasMaxLength(400);
        }
    }
}