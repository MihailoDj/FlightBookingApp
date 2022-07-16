using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Entities;

namespace api.DTO
{
    public class FlightDTO
    {
        public int Id { get; set; }
        public DateTime DateOfFlight { get; set; }
        public int DepartureCityId { get; set; }
        public int ArrivalCityId { get; set; }
        public int TotalSeats { get; set; }
        public int RemainingSeats { get; set; }
        public int ConnectingFlights { get; set; }
        public ICollection<Reservation>? Reservations { get; set; }
    }
}