using Nop.Core.Domain.Catalog;

namespace Nop.Data.Mapping.Catalog
{
    public class CrossSellProductMap : GoqEntityTypeConfiguration<CrossSellProduct>
    {
        public CrossSellProductMap()
        {
            ToTable("CrossSellProduct");
            HasKey(c => c.Id);
        }
    }
}