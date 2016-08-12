using Nop.Core.Domain.Directory;

namespace Nop.Data.Mapping.Directory
{
    public class CountryMap : GoqEntityTypeConfiguration<Country>
    {
        public CountryMap()
        {
            ToTable("Country");
            HasKey(c =>c.Id);
            Property(c => c.Name).IsRequired().HasMaxLength(100);
            Property(c =>c.TwoLetterIsoCode).HasMaxLength(2);
            Property(c =>c.ThreeLetterIsoCode).HasMaxLength(3);
        }
    }
}