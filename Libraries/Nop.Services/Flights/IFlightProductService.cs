using System.Collections.Generic;
using Nop.Core.Data;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Flights;

namespace Nop.Services.Flights
{
    public interface IFlightProductService
    {
        FlightProduct GetProductById(int id);

        #region Meals

        IEnumerable<FlightProduct> GetAllMeals();

        void InsertMeal(FlightProduct flight, Product product);

        #endregion
    }

    public class FlightProductService : IFlightProductService
    {
        private readonly IRepository<FlightProduct> _repository;
        private readonly IRepository<Product> _productRepository;

        public FlightProductService(IRepository<FlightProduct> repository, IRepository<Product> productRepository)
        {
            _repository = repository;
            _productRepository = productRepository;
        }

        public FlightProduct GetProductById(int id)
        {
            return id > 0 ? _repository.GetById(id) : null;
        }


        public IEnumerable<FlightProduct> GetAllMeals()
        {
            return _repository.Find(x => !x.Deleted && x.Type == FlightProductType.Meal);
        }

        public void InsertMeal(FlightProduct flight, Product product)
        {
            if (flight != null && flight.Type == FlightProductType.Meal && product != null)
            {
                _productRepository.Insert(product);
                flight.Product = product;
                _repository.Insert(flight);
            }
        }
    }
}
