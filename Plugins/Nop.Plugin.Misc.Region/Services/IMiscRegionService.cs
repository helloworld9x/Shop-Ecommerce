using Nop.Plugin.Misc.Region.Domain;

namespace Nop.Plugin.Misc.Region.Services
{
    public interface IMiscRegionService
    {
        void InsertProductRestriction(ProductRegion productRegion);

        ProductRegion GetProductRegionbyId(int id);

         bool CountryRestrictionExists(int productId, int countryId);

        ProductRegion GetProductRegionbyId(int productId, int countryId);

        void RemoveProductRegionbyId(int id);

        void UpdateProductRegionbyId(int id);

        
    }
}
