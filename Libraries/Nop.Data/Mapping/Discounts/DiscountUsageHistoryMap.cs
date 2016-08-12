using Nop.Core.Domain.Discounts;

namespace Nop.Data.Mapping.Discounts
{
    public partial class DiscountUsageHistoryMap : GoqEntityTypeConfiguration<DiscountUsageHistory>
    {
        public DiscountUsageHistoryMap()
        {
            this.ToTable("DiscountUsageHistory");
            this.HasKey(duh => duh.Id);
            
            this.HasRequired(duh => duh.Discount)
                .WithMany()
                .HasForeignKey(duh => duh.DiscountId);

            this.HasRequired(duh => duh.Order)
                .WithMany(o => o.DiscountUsageHistory)
                .HasForeignKey(duh => duh.OrderId);
        }
    }
}