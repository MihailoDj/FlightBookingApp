import { Component, OnInit } from '@angular/core';
import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { FlightModalComponent } from 'src/app/flight-modal/flight-modal.component';
import { City } from 'src/app/_models/city';
import { Flight } from 'src/app/_models/flight';
import { Reservation } from 'src/app/_models/reservation';
import { CityService } from 'src/app/_services/city.service';
import { FlightService } from 'src/app/_services/flight.service';
import { ReservationService } from 'src/app/_services/reservation.service';

export class Model {
  departureCityId: number;
  arrivalCityId: number;
  hasConnectingFlights: boolean
}

@Component({
  selector: 'app-visitor-main',
  templateUrl: './visitor-main.component.html',
  styleUrls: ['./visitor-main.component.css'],
})
export class VisitorMainComponent implements OnInit {
  cities: City[] = [];
  reservations: Reservation[] = [];
  flights: Flight[] = [];
  model: Model = {departureCityId: 1, arrivalCityId: 2, hasConnectingFlights: false};
  bsModalRef?: BsModalRef;

  constructor(
    private reservationService: ReservationService,
    private flightService: FlightService,
    private cityService: CityService,
    private modalService: BsModalService
  ) {}

  ngOnInit(): void {
    this.getReservationsForUser();
    this.getCities();
  }

  cancelReservation(flightId: number, userId: number) {
    this.reservationService.deleteReservation(flightId, userId).subscribe(response => {
      console.log(response);
      this.getReservationsForUser();
    }, error => {
      console.log(error);
    });
  }

  getFilteredFlights() {
    console.log(this.model);
    this.flightService.getFlights(this.model).subscribe(response => {
      this.flights = response;
    },error => {
      console.log(error);
    });
  }

  openFlightSeats(flight: Flight) {
    const initialState: ModalOptions = {
      initialState: {
        flight: flight,
        title: 'Pick an available seat',
      },
      class: 'modal-lg justify-content-center text-center'
    };
    this.bsModalRef = this.modalService.show(
      FlightModalComponent,
      initialState
    );
  }

  changeSelection(){
    this.model.hasConnectingFlights = !this.model.hasConnectingFlights;
  }

  getReservationsForUser() {
    this.reservationService.getReservationsForUser().subscribe(
      (response) => {
        this.reservations = response;
      },
      (error) => {
        console.log(error);
      }
    );
  }

  getCities() {
    this.cityService.getCities().subscribe(
      (response) => {
        this.cities = response;
      },
      (error) => {
        console.log(error);
      }
    );
  }
}
