using Nop.Core.Domain.Flights;

namespace Nop.Data.Mapping.Flights
{
    public class FlightProductMap : GoqEntityTypeConfiguration<FlightProduct>
    {
        public FlightProductMap()
        {
            ToTable("FlightProduct");
            HasKey(d => d.Id);
            HasRequired(u => u.Product);
            Property(d => d.Code).IsRequired().HasMaxLength(50);
            Property(d => d.Attributes).IsMaxLength();
            Property(d => d.Name).IsRequired().HasMaxLength(100);
            Property(d => d.Price).IsRequired().HasPrecision(18, 6);
        }
    }
}
