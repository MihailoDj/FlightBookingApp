using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace api.SignalR
{
    public class MainHub : Hub
    {
        private readonly IFlightRepository _flightRepository;

        public MainHub(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }

        public async Task GetFlights()
        {
            var flights = await _flightRepository.GetFlightsAsync(null);
            await Clients.All.SendAsync("GetFlights", flights);
        }
    }
}