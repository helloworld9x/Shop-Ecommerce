using Nop.Core.Domain.Catalog;

namespace Nop.Data.Mapping.Catalog
{
    public class PredefinedProductAttributeValueMap : GoqEntityTypeConfiguration<PredefinedProductAttributeValue>
    {
        public PredefinedProductAttributeValueMap()
        {
            ToTable("PredefinedProductAttributeValue");
            HasKey(pav => pav.Id);
            Property(pav => pav.Name).IsRequired().HasMaxLength(400);

            Property(pav => pav.PriceAdjustment).HasPrecision(18, 4);
            Property(pav => pav.WeightAdjustment).HasPrecision(18, 4);
            Property(pav => pav.Cost).HasPrecision(18, 4);

            HasRequired(pav => pav.ProductAttribute)
                .WithMany()
                .HasForeignKey(pav => pav.ProductAttributeId);
        }
    }
}