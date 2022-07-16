using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO;
using api.Entities;
using api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    public class ReservationsController : BaseApiController
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IFlightRepository _flightRepository;

        public ReservationsController(
            IReservationRepository reservationRepository,
            IUserRepository userRepository,
            IFlightRepository flightRepository)
        {
            _reservationRepository = reservationRepository;
            _userRepository = userRepository;
            _flightRepository=flightRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Reservation>> GetReservations()
        {
            return await _reservationRepository.GetReservationsAsync();
        }

        [HttpGet("user/{userId}")]
        public async Task<IEnumerable<Reservation>> GetReservationsForUser(int userId)
        {
            return await _reservationRepository.GetReservationsForUser(userId);
        }

        [HttpDelete("delete/{flightId}/{userId}")]
        public async Task<ActionResult> DeleteReservation(int flightId, int userId)
        {
            var reservation = await _reservationRepository.FindReservation(flightId, userId);

            _reservationRepository.DeleteReservation(reservation);

            if (await _reservationRepository.SaveAllAsync())
            {
                if (reservation.Status != Enums.ReservationStatus.Rejected)
                {
                    var flight = await _flightRepository.GetFlightById(reservation.FlightId);
                    flight.RemainingSeats++;

                    _flightRepository.UpdateFlight(flight);
                    await _flightRepository.SaveAllAsync();
                }
                return Ok();
            }

            return BadRequest("Unable to delete reservation");
        }

        [HttpPut("update")]
        public async Task<ActionResult> UpdateReservation(ReservationDTO reservationDTO)
        {
            var reservation = await _reservationRepository.FindReservation(
                reservationDTO.FlightId, reservationDTO.UserId);

            reservation.Status = reservationDTO.Status;

            _reservationRepository.UpdateReservation(reservation);

            if (!await _reservationRepository.SaveAllAsync())
                return BadRequest("Unable to update reservation");

            if (reservation.Status == Enums.ReservationStatus.Rejected) {
                var flight = await _flightRepository.GetFlightById(reservationDTO.FlightId);

                flight.RemainingSeats++;
                _flightRepository.UpdateFlight(flight);

                await _flightRepository.SaveAllAsync();
            }

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> AddReservation(ReservationDTO reservationDTO)
        {
            var user = await _userRepository.GetUserByIdAsync(reservationDTO.UserId);
            var flight = await _flightRepository.GetFlightById(reservationDTO.FlightId);

            var reservation = new Reservation
            {
                User = user,
                UserId = user.Id,
                Flight = flight,
                FlightId = flight.Id,
                SeatNumber = reservationDTO.SeatNumber,
                Status = (Enums.ReservationStatus)reservationDTO.Status
            };

            if(flight.RemainingSeats <= 0)
                return BadRequest("No seats left on this flight.");

            _reservationRepository.AddReservation(reservation);

            if (await _reservationRepository.SaveAllAsync())
            {
                flight.RemainingSeats--;
                _flightRepository.UpdateFlight(flight);
                await _flightRepository.SaveAllAsync();

                return Ok();
            }

            return BadRequest("Unable to add reservation.");
        }
    }
}