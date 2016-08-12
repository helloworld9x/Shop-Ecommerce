using Nop.Core.Domain.Catalog;

namespace Nop.Data.Mapping.Catalog
{
    public class ProductSpecificationAttributeMap : GoqEntityTypeConfiguration<ProductSpecificationAttribute>
    {
        public ProductSpecificationAttributeMap()
        {
            ToTable("Product_SpecificationAttribute_Mapping");
            HasKey(psa => psa.Id);

            Property(psa => psa.CustomValue).HasMaxLength(4000);

            Ignore(psa => psa.AttributeType);

            HasRequired(psa => psa.SpecificationAttributeOption)
                .WithMany()
                .HasForeignKey(psa => psa.SpecificationAttributeOptionId);


            HasRequired(psa => psa.Product)
                .WithMany(p => p.ProductSpecificationAttributes)
                .HasForeignKey(psa => psa.ProductId);
        }
    }
}