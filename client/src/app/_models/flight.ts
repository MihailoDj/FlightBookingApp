import { City } from "./city";
import { Reservation } from "./reservation";

export interface Flight {
    id: number,
    departureCity: City,
    arrivalCity: City,
    dateOfFlight: Date,
    totalSeats: number,
    remainingSeats: number,
    connectingFlights: number,
    reservations: Reservation[]
}