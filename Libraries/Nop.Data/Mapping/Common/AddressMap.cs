using Nop.Core.Domain.Common;

namespace Nop.Data.Mapping.Common
{
    public class AddressMap : GoqEntityTypeConfiguration<Address>
    {
        public AddressMap()
        {
            ToTable("Address");
            HasKey(a => a.Id);

            HasOptional(a => a.Country)
                .WithMany()
                .HasForeignKey(a => a.CountryId).WillCascadeOnDelete(false);

            HasOptional(a => a.StateProvince)
                .WithMany()
                .HasForeignKey(a => a.StateProvinceId).WillCascadeOnDelete(false);
        }
    }
}
