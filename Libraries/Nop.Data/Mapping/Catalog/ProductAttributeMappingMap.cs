using Nop.Core.Domain.Catalog;

namespace Nop.Data.Mapping.Catalog
{
    public class ProductAttributeMappingMap : GoqEntityTypeConfiguration<ProductAttributeMapping>
    {
        public ProductAttributeMappingMap()
        {
            ToTable("Product_ProductAttribute_Mapping");
            HasKey(pam => pam.Id);
            Ignore(pam => pam.AttributeControlType);

            HasRequired(pam => pam.Product)
                .WithMany(p => p.ProductAttributeMappings)
                .HasForeignKey(pam => pam.ProductId);

            HasRequired(pam => pam.ProductAttribute)
                .WithMany()
                .HasForeignKey(pam => pam.ProductAttributeId);
        }
    }
}