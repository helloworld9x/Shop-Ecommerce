using Nop.Core.Domain.Catalog;

namespace Nop.Data.Mapping.Catalog
{
    public class ProductAttributeMap : GoqEntityTypeConfiguration<ProductAttribute>
    {
        public ProductAttributeMap()
        {
            ToTable("ProductAttribute");
            HasKey(pa => pa.Id);
            Property(pa => pa.Name).IsRequired();
        }
    }
}