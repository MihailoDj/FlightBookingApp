using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO;
using api.Entities;
using api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Authorize]
    public class FlightsController : BaseApiController
    {
        private readonly IFlightRepository _flightRepository;
        private readonly ICityRepository _cityRepository;

        public FlightsController(IFlightRepository flightRepository,ICityRepository cityRepository)
        {
            _flightRepository = flightRepository;
            _cityRepository = cityRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Flight>> GetFlights([FromQuery]FlightFiltersDTO filters)
        {
            return await _flightRepository.GetFlightsAsync(filters);
        }

        [HttpPost("add")]
        public async Task<ActionResult<Flight>> AddFlight(FlightDTO flightDTO)
        {

            var departureCity = await _cityRepository.GetCityById(flightDTO.DepartureCityId);

            var arrivalCity = await _cityRepository.GetCityById(flightDTO.ArrivalCityId);

            var flight = new Flight
            {
                Id = flightDTO.Id,
                DepartureCity = departureCity,
                ArrivalCity = arrivalCity,
                DateOfFlight = flightDTO.DateOfFlight,
                TotalSeats = flightDTO.TotalSeats,
                RemainingSeats = flightDTO.TotalSeats,
                ConnectingFlights = flightDTO.ConnectingFlights
            };

            _flightRepository.AddFlight(flight);
            await _flightRepository.SaveAllAsync();

            return flight;
        }

        [HttpDelete("delete/{flightId}")]
        public async Task<ActionResult> DeleteFlight(int flightId)
        {
            var flight = await _flightRepository.GetFlightById(flightId);

            _flightRepository.Remove(flight);

            if (await _flightRepository.SaveAllAsync())
                return Ok();

            return BadRequest("Unable to delete flight.");
        }
    }
}