using Nop.Core.Domain.Catalog;

namespace Nop.Data.Mapping.Catalog
{
    public class RelatedProductMap : GoqEntityTypeConfiguration<RelatedProduct>
    {
        public RelatedProductMap()
        {
            ToTable("RelatedProduct");
            HasKey(c => c.Id);
        }
    }
}