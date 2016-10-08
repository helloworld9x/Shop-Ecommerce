using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Flights;

namespace Nop.Data.Mapping.Flights
{
    public class FlightStatusMap : GoqEntityTypeConfiguration<FlightStatus>
    {
        public FlightStatusMap()
        {
            ToTable("FlightStatus");
            HasKey(d => d.Id);
            Property(d => d.CommercialName).IsRequired().HasMaxLength(200);
            Property(d => d.AirlineCode).HasMaxLength(5);
            Property(d => d.AirlineName).HasMaxLength(100);
            Property(d => d.Currency).HasMaxLength(5);
            Property(d => d.FlightNumber).HasMaxLength(10);
            Property(d => d.From).HasMaxLength(10);
            Property(d => d.To).HasMaxLength(10);

           
          
        }
    }
}
