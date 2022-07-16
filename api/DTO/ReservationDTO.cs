using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Enums;

namespace api.DTO
{
    public class ReservationDTO
    {
        public int FlightId { get; set; }
        public int UserId { get; set; }
        public ReservationStatus Status { get; set; }
        public int SeatNumber { get; set; }
    }
}