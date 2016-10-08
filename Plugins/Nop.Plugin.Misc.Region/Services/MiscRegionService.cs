using System.Linq;
using Nop.Core.Data;
using Nop.Plugin.Misc.Region.Domain;

namespace Nop.Plugin.Misc.Region.Services
{
    public class MiscRegionService : IMiscRegionService
    {
        private readonly IRepository<ProductRegion> _productRegionRepository;

        public MiscRegionService(IRepository<ProductRegion> productRegionRepository)
        {
            _productRegionRepository = productRegionRepository;
        }

        public void InsertProductRestriction(ProductRegion productRegion)
        {
            _productRegionRepository.Insert(productRegion);
        }

        public ProductRegion GetProductRegionbyId(int id)
        {
            if (id == 0)
            {
                return null;
            }
            return _productRegionRepository.GetById(id);
        }

        public bool CountryRestrictionExists(int productId, int countryId)
        {
            if (productId > 0 && countryId > 0)
            {
                var product = _productRegionRepository.FindOne(x => x.ProductId == productId && x.RegionId == countryId);
                return product != null;
            }
            return false;
        }

        public ProductRegion GetProductRegionbyId(int productId, int countryId)
        {
            var product = _productRegionRepository.FindOne(x => x.ProductId == productId && x.RegionId == countryId);
            return product;
        }
        public void RemoveProductRegionbyId(int id)
        {
            if (id > 0 && id < int.MaxValue)
            {
                var itemRemove = _productRegionRepository.GetById(id);
                if (itemRemove != null)
                    _productRegionRepository.Delete(itemRemove);
            }
        }

        public void UpdateProductRegionbyId(int id)
        {
            if (id > 0 && id < int.MaxValue)
            {
                var itemUpdate = _productRegionRepository.GetById(id);
                if (itemUpdate != null)
                    _productRegionRepository.Update(itemUpdate);
            }
        }
    }
}
