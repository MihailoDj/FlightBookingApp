using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO;
using api.Entities;

namespace api.Interfaces
{
    public interface IFlightRepository
    {
        Task<IEnumerable<Flight>> GetFlightsAsync(FlightFiltersDTO filters);
        void AddFlight(Flight flight);
        void UpdateFlight(Flight flight);
        Task<bool> SaveAllAsync();
        Task<Flight> GetFlightById(int id);
        void Remove(Flight flight);
    }
}