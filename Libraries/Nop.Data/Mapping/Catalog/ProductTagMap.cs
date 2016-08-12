using Nop.Core.Domain.Catalog;

namespace Nop.Data.Mapping.Catalog
{
    public class ProductTagMap : GoqEntityTypeConfiguration<ProductTag>
    {
        public ProductTagMap()
        {
            ToTable("ProductTag");
            HasKey(pt => pt.Id);
            Property(pt => pt.Name).IsRequired().HasMaxLength(400);
        }
    }
}