using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Entities;

namespace api.Interfaces
{
    public interface IReservationRepository
    {
        Task<bool> SaveAllAsync();
        Task<IEnumerable<Reservation>> GetReservationsAsync();
        Task<IEnumerable<Reservation>> GetReservationsForUser(int userId);
        void AddReservation(Reservation reservation);
        void DeleteReservation(Reservation reservation);
        void UpdateReservation(Reservation reservation);
        Task<Reservation> FindReservation(int flightId, int userId);
    }
}