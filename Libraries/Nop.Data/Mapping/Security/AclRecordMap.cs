using Nop.Core.Domain.Security;

namespace Nop.Data.Mapping.Security
{
    public class AclRecordMap : GoqEntityTypeConfiguration<AclRecord>
    {
        public AclRecordMap()
        {
            ToTable("AclRecord");
            HasKey(ar => ar.Id);

            Property(ar => ar.EntityName).IsRequired().HasMaxLength(400);

            HasRequired(ar => ar.CustomerRole)
                .WithMany()
                .HasForeignKey(ar => ar.CustomerRoleId)
                .WillCascadeOnDelete(true);
        }
    }
}