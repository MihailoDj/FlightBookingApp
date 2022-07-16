using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Entities
{
    public class Flight
    {
        public int Id { get; set; }
        public DateTime DateOfFlight { get; set; }
        public City DepartureCity { get; set; }
        public City ArrivalCity { get; set; }
        public int TotalSeats { get; set; }
        public int RemainingSeats { get; set; }
        public int ConnectingFlights { get; set; }
        public ICollection<Reservation>? Reservations { get; set; }
    }
}