using Nop.Core.Domain.Catalog;

namespace Nop.Data.Mapping.Catalog
{
    public class CategoryMap : GoqEntityTypeConfiguration<Category>
    {
        public CategoryMap()
        {
            ToTable("Category");
            HasKey(c => c.Id);
            Property(c => c.Name).IsRequired().HasMaxLength(400);
            Property(c => c.PriceRanges).HasMaxLength(400);
            Property(c => c.PageSizeOptions).HasMaxLength(200);
        }
    }
}