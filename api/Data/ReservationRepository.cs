using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Entities;
using api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly DataContext _context;

        public ReservationRepository(DataContext context)
        {
            _context = context;
        }

        public void DeleteReservation(Reservation reservation)
        {
            _context.Reservations.Remove(reservation);
        }

        public async Task<Reservation> FindReservation(int flightId, int userId)
        {
            return await _context.Reservations.FirstOrDefaultAsync(
                r => r.FlightId == flightId && r.UserId == userId);
        }

        public async Task<IEnumerable<Reservation>> GetReservationsForUser(int userId)
        {
            return await _context.Reservations
                .Where(r => r.UserId == userId)
                .Include(r => r.Flight).ThenInclude(f => f.DepartureCity)
                .Include(r => r.Flight).ThenInclude(f => f.ArrivalCity)
                .Include(r => r.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetReservationsAsync()
        {
            return await _context.Reservations
                .Include(r => r.Flight)
                .Include(r => r.User)
                .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void UpdateReservation(Reservation reservation)
        {
            _context.Reservations.Update(reservation);
        }

        public void AddReservation(Reservation reservation)
        {
            _context.Add(reservation);
        }
    }
}