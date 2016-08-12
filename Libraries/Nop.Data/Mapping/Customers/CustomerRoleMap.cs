using Nop.Core.Domain.Customers;

namespace Nop.Data.Mapping.Customers
{
    public class CustomerRoleMap : GoqEntityTypeConfiguration<CustomerRole>
    {
        public CustomerRoleMap()
        {
            ToTable("CustomerRole");
            HasKey(cr => cr.Id);
            Property(cr => cr.Name).IsRequired().HasMaxLength(255);
            Property(cr => cr.SystemName).HasMaxLength(255);
        }
    }
}