import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { City } from 'src/app/_models/city';
import { Flight } from 'src/app/_models/flight';
import { Reservation } from 'src/app/_models/reservation';
import { CityService } from 'src/app/_services/city.service';
import { FlightService } from 'src/app/_services/flight.service';
import { LiveReloadService } from 'src/app/_services/live-reload.service';
import { ReservationService } from 'src/app/_services/reservation.service';

@Component({
  selector: 'app-agent-main',
  templateUrl: './agent-main.component.html',
  styleUrls: ['./agent-main.component.css'],
})
export class AgentMainComponent implements OnInit {
  flights: Flight[] = [];
  cities: City[] = [];
  reservations: Reservation[] = [];
  model: any = {
    departureCityId: 1,
    arrivalCityId: 2, 
    dateOfFlight: new Date()
  };
  minDate = new Date();
  bsValue: Date;
  @ViewChild('flightForm', { static: true }) flightForm!: NgForm;
  errorMessage: string;

  constructor(
    private flightService: FlightService,
    private cityService: CityService,
    private reservationService: ReservationService,
    private liveReloadService: LiveReloadService
  ) {}

  ngOnInit(): void {
    this.getCities();
    this.getFlights();
    this.getReservations();
  }

  getReservations() {
    this.reservationService.getReservations().subscribe(
      (response) => {
        this.reservations = response;
        console.log(response);
      },
      (error) => {
        console.log(error);
      }
    );
  }

  acceptReservation(flightId: number, userId: number) {
    const resModel = {
      flightId: flightId,
      userId: userId,
      status: 2,
    };

    this.updateReservation(resModel);
  }

  rejectReservation(flightId: number, userId: number) {
    const resModel = {
      flightId: flightId,
      userId: userId,
      status: 3,
    };

    this.updateReservation(resModel);
  }

  updateReservation(resModel: any) {
    this.reservationService.updateReservation(resModel).subscribe(
      (response) => {
        this.getReservations();
      },
      (error) => {}
    );
  }

  addFlight() {
    if (this.model.arrivalCityId === this.model.departureCityId) {
      this.errorMessage = "Arrival and departure city can't be the same.";
      return;
    }

    if (this.flightForm.invalid){
      this.errorMessage = "All fields are required.";
      return;
    }

    this.flightService.addFlight(this.model).subscribe(
      () => {
        alert('Flight successfully added');
        this.getFlights();
      },
      (error) => {
        this.errorMessage = error.error.title;
        console.log(error);
      }
    );
  }

  getCities() {
    this.cityService.getCities().subscribe((response) => {
      this.cities = response;
    });
  }

  getFlights() {
    this.liveReloadService.thenableConnection.then(() => {
      this.liveReloadService.hubConnection.invoke('GetFlights');

      this.flightService.flights.subscribe(f =>{
        this.flights = f;
      });
    });
    // this.flightService.getFlights().subscribe((response) => {
    //   console.log(response);

    //   this.flights = response;
    // });
  }
}
