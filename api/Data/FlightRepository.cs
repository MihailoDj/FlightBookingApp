using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO;
using api.Entities;
using api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class FlightRepository : IFlightRepository
    {
        private readonly DataContext _context;

        public FlightRepository(DataContext context)
        {
            _context = context;
        }

        public void AddFlight(Flight flight)
        {
            _context.Flights.Add(flight);
        }

        public async Task<Flight> GetFlightById(int id)
        {
            return await _context.Flights.FindAsync(id);
        }

        public async Task<IEnumerable<Flight>> GetFlightsAsync(FlightFiltersDTO filters)
        {
            var flights = _context.Flights
                .Include(f => f.ArrivalCity)
                .Include(f => f.DepartureCity)
                .Include(f => f.Reservations)
                .AsQueryable();

            if (filters == null || filters.ArrivalCityId == 0 || filters.DepartureCityId == 0)
                return await flights.ToListAsync();

            return await flights.Where(
                f => f.ArrivalCity.Id == filters.ArrivalCityId 
                && f.DepartureCity.Id == filters.DepartureCityId
                && f.ConnectingFlights == (filters.HasConnectingFlights ? f.ConnectingFlights: 0)
                && f.RemainingSeats > 0
                && f.DateOfFlight >= DateTime.Now.AddDays(3))
                .ToListAsync();
        
        }

        public void Remove(Flight flight)
        {
            _context.Flights.Remove(flight);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void UpdateFlight(Flight flight)
        {
            _context.Flights.Update(flight);
        }
    }
}