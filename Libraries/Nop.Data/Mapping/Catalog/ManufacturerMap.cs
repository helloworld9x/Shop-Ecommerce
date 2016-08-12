using Nop.Core.Domain.Catalog;

namespace Nop.Data.Mapping.Catalog
{
    public class ManufacturerMap : GoqEntityTypeConfiguration<Manufacturer>
    {
        public ManufacturerMap()
        {
            ToTable("Manufacturer");
            HasKey(m => m.Id);
            Property(m => m.Name).IsRequired().HasMaxLength(400);
            Property(m => m.PriceRanges).HasMaxLength(400);
            Property(m => m.PageSizeOptions).HasMaxLength(200);
        }
    }
}