using Nop.Core.Domain.Catalog;

namespace Nop.Data.Mapping.Catalog
{
    public class ProductAttributeValueMap : GoqEntityTypeConfiguration<ProductAttributeValue>
    {
        public ProductAttributeValueMap()
        {
            ToTable("ProductAttributeValue");
            HasKey(pav => pav.Id);
            Property(pav => pav.Name).IsRequired().HasMaxLength(400);
            Property(pav => pav.ColorSquaresRgb).HasMaxLength(100);

            Property(pav => pav.PriceAdjustment).HasPrecision(18, 4);
            Property(pav => pav.WeightAdjustment).HasPrecision(18, 4);
            Property(pav => pav.Cost).HasPrecision(18, 4);

            Ignore(pav => pav.AttributeValueType);

            HasRequired(pav => pav.ProductAttributeMapping)
                .WithMany(pam => pam.ProductAttributeValues)
                .HasForeignKey(pav => pav.ProductAttributeMappingId);
        }
    }
}