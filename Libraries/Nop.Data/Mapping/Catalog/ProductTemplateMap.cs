using Nop.Core.Domain.Catalog;

namespace Nop.Data.Mapping.Catalog
{
    public class ProductTemplateMap : GoqEntityTypeConfiguration<ProductTemplate>
    {
        public ProductTemplateMap()
        {
            ToTable("ProductTemplate");
            HasKey(p => p.Id);
            Property(p => p.Name).IsRequired().HasMaxLength(400);
            Property(p => p.ViewPath).IsRequired().HasMaxLength(400);
        }
    }
}