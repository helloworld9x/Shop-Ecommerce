using Nop.Core.Domain.Customers;

namespace Nop.Data.Mapping.Customers
{
    public  class ExternalAuthenticationRecordMap : GoqEntityTypeConfiguration<ExternalAuthenticationRecord>
    {
        public ExternalAuthenticationRecordMap()
        {
            ToTable("ExternalAuthenticationRecord");

            HasKey(ear => ear.Id);

            HasRequired(ear => ear.Customer)
                .WithMany(c => c.ExternalAuthenticationRecords)
                .HasForeignKey(ear => ear.CustomerId);

        }
    }
}