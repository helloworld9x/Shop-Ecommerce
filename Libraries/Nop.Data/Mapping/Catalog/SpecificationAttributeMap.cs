using Nop.Core.Domain.Catalog;

namespace Nop.Data.Mapping.Catalog
{
    public class SpecificationAttributeMap : GoqEntityTypeConfiguration<SpecificationAttribute>
    {
        public SpecificationAttributeMap()
        {
            ToTable("SpecificationAttribute");
            HasKey(sa => sa.Id);
            Property(sa => sa.Name).IsRequired();
        }
    }
}