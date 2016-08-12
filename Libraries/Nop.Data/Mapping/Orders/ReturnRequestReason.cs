using Nop.Core.Domain.Orders;

namespace Nop.Data.Mapping.Orders
{
    public class ReturnRequestReasonMap : GoqEntityTypeConfiguration<ReturnRequestReason>
    {
        public ReturnRequestReasonMap()
        {
            this.ToTable("ReturnRequestReason");
            this.HasKey(rrr => rrr.Id);
            this.Property(rrr => rrr.Name).IsRequired().HasMaxLength(400);
        }
    }
}