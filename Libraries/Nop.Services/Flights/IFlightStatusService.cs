using System.Collections.Generic;
using Nop.Core.Data;
using Nop.Core.Domain.Flights;
using Nop.Data;

namespace Nop.Services.Flights
{
    public interface IFlightStatusService
    {
        /// <summary>
        /// Delete flight
        /// </summary>
        /// <param name="flight">FlightStatus</param>
        void DeleteFlightStatus(FlightStatus flight);

        void InsertFLightStatus(FlightStatus flight);

        void UpdateFLightStatus(FlightStatus flight);

        FlightStatus GetFlightById(int id);

        IEnumerable<FlightStatus> GetAllFlightStatus();

    }

    public class FlightStatusService : IFlightStatusService
    {
        private readonly IRepository<FlightStatus> _repository;

        public FlightStatusService(IRepository<FlightStatus> repository)
        {
            _repository = repository;
        }

        public void DeleteFlightStatus(FlightStatus flight)
        {
            if (flight != null)
            {
                flight.Active = false;
                flight.Deleted = true;
                _repository.Update(flight);
            }
        }

        public void InsertFLightStatus(FlightStatus flight)
        {
            if (flight != null)
            {
                _repository.Insert(flight);
            }
        }

        public void UpdateFLightStatus(FlightStatus flight)
        {
            if (flight != null && flight.Id > 0)
            {
                _repository.Update(flight);
            }
        }

        public FlightStatus GetFlightById(int id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<FlightStatus> GetAllFlightStatus()
        {
            return _repository.Find(x => !x.Deleted);
        }
    }
}
