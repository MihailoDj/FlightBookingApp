using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTO
{
    public class FlightFiltersDTO
    {
        public int DepartureCityId { get; set; }
        public int ArrivalCityId { get; set; }
        public bool HasConnectingFlights { get; set; }
    }
}