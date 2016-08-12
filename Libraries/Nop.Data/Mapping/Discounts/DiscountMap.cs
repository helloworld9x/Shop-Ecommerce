using Nop.Core.Domain.Discounts;

namespace Nop.Data.Mapping.Discounts
{
    public class DiscountMap : GoqEntityTypeConfiguration<Discount>
    {
        public DiscountMap()
        {
            ToTable("Discount");
            HasKey(d => d.Id);
            Property(d => d.Name).IsRequired().HasMaxLength(200);
            Property(d => d.CouponCode).HasMaxLength(100);
            Property(d => d.DiscountPercentage).HasPrecision(18, 4);
            Property(d => d.DiscountAmount).HasPrecision(18, 4);
            Property(d => d.MaximumDiscountAmount).HasPrecision(18, 4);

            Ignore(d => d.DiscountType);
            Ignore(d => d.DiscountLimitation);

            HasMany(dr => dr.AppliedToCategories)
                .WithMany(c => c.AppliedDiscounts)
                .Map(m => m.ToTable("Discount_AppliedToCategories"));
            
            HasMany(dr => dr.AppliedToProducts)
                .WithMany(p => p.AppliedDiscounts)
                .Map(m => m.ToTable("Discount_AppliedToProducts"));
        }
    }
}