using Nop.Core.Domain.Catalog;

namespace Nop.Data.Mapping.Catalog
{
    public class SpecificationAttributeOptionMap : GoqEntityTypeConfiguration<SpecificationAttributeOption>
    {
        public SpecificationAttributeOptionMap()
        {
            ToTable("SpecificationAttributeOption");
            HasKey(sao => sao.Id);
            Property(sao => sao.Name).IsRequired();
            
            HasRequired(sao => sao.SpecificationAttribute)
                .WithMany(sa => sa.SpecificationAttributeOptions)
                .HasForeignKey(sao => sao.SpecificationAttributeId);
        }
    }
}