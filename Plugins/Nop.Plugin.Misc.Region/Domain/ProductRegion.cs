using Nop.Core;

namespace Nop.Plugin.Misc.Region.Domain
{
   public class ProductRegion : BaseEntity
    {
        public int? ProductId { get; set; }

        public int? RegionId { get; set; }
    }
}
