using Nop.Data.Mapping;
using Nop.Plugin.Misc.Region.Domain;

namespace Nop.Plugin.Misc.Region.Data
{
    public class MiscRegionMap : GoqEntityTypeConfiguration<ProductRegion>
    {
        public MiscRegionMap()
        {
            ToTable("ProductRegion");
            HasKey(x => x.Id);
        }
    }
}
