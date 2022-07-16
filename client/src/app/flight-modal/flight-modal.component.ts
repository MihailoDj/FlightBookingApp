import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Flight } from '../_models/flight';
import { AccountService } from '../_services/account.service';
import { ReservationService } from '../_services/reservation.service';

@Component({
  selector: 'app-flight-modal',
  templateUrl: './flight-modal.component.html',
  styleUrls: ['./flight-modal.component.css']
})
export class FlightModalComponent implements OnInit {
  title: string;
  flight: Flight;
  model: any;

  constructor(public bsModalRef: BsModalRef, private reservationService: ReservationService,
    private accountService: AccountService) { }

  ngOnInit(): void {
  }

  makeReservation(seatNumber: number) {
    this.model = {
      flightId: this.flight.id,
      userId: this.accountService.currentUser.id,
      status: 1,
      seatNumber: seatNumber
    };

    this.reservationService.addReservation(this.model).subscribe(response => {
      alert("Reservation successfully added.");
      this.bsModalRef.hide();
    }, error => {
      alert("You already booked a seat on this flight.");
      console.log(error);
    });
  }

  seatAvailable(seatNumber: number) {
    const reservations = this.flight.reservations;
    const res = reservations.find(r => r.seatNumber === seatNumber);

    if (res === undefined || res.status === 3)
      return true;
    return false;
  }

}
