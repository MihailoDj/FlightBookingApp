using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Enums;

namespace api.Entities
{
    public class Reservation
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int FlightId { get; set; }
        public Flight Flight { get; set; }
        public int SeatNumber { get; set; }
        public ReservationStatus Status { get; set; }
    }
}