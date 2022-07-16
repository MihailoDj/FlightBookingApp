import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Flight } from 'src/app/_models/flight';
import { AccountService } from 'src/app/_services/account.service';
import { FlightService } from 'src/app/_services/flight.service';
import { LiveReloadService } from 'src/app/_services/live-reload.service';

@Component({
  selector: 'app-admin-main',
  templateUrl: './admin-main.component.html',
  styleUrls: ['./admin-main.component.css'],
})
export class AdminMainComponent implements OnInit {
  model: any = {
    role: 1
  };
  @ViewChild('registerForm', { static: true }) registerForm!: NgForm;
  flights: Flight[] = [];
  errorMessage: string;

  constructor(
    private accountService: AccountService,
    private flightService: FlightService,
    private liveReloadService: LiveReloadService
  ) {}

  ngOnInit(): void {
    this.getFlights();
  }

  getFlights() {
    this.liveReloadService.thenableConnection.then(() => {
      this.liveReloadService.hubConnection.invoke('GetFlights');

      this.flightService.flights.subscribe(f =>{
        this.flights = f;
      });
    });
    // this.flightService.getFlights().subscribe((response) => {
    //   this.flights = response;
    // });
    
  }

  deleteFlight(flightId: number) {
    this.flightService.deleteFlight(flightId).subscribe(response => {
      console.log(response);

      this.getFlights();

    });
  }

  registerUser() {
    if (this.registerForm.invalid){
      this.errorMessage = 'Invalid input.';
      return;
    }

    this.accountService.registerUser(this.model).subscribe(
      (response) => {
        if (response)
          alert(`User ${response.userName} successfully registered`);

        this.registerForm.resetForm();
      },
      (error) => {
        this.errorMessage = error.error.title;
        console.log(error);
      }
    );
  }
}
