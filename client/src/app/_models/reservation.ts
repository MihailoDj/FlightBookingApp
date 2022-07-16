import { Flight } from "./flight";
import { User } from "./user";

export interface Reservation {
    user: User,
    flight: Flight,
    status: number,
    seatNumber: number
}