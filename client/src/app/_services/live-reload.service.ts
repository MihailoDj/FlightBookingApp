import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { environment } from 'src/environments/environment';
import { FlightService } from './flight.service';

@Injectable({
  providedIn: 'root',
})
export class LiveReloadService {
  hubUrl = environment.hubUrl;
  hubConnection: HubConnection = new HubConnectionBuilder()
    .withUrl(this.hubUrl)
    .withAutomaticReconnect()
    .build();
  thenableConnection:Promise<void>;

  constructor(private flightService: FlightService) {}

  createHubConnection() {
    this.thenableConnection = this.hubConnection
      .start()
      .catch((error) => console.log(error));

    this.hubConnection.on('GetFlights', (flights) => {
      console.log(flights);
      this.flightService.flightsSource.next(flights);
    });
  }

  stopHubConnection() {
    this.hubConnection.stop().catch((error) => console.log(error));
  }
}
