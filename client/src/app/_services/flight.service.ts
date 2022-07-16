import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Flight } from '../_models/flight';

@Injectable({
  providedIn: 'root'
})
export class FlightService {
  baseUrl: string = environment.apiUrl;
  flightsSource = new BehaviorSubject<Flight[]>([]);
  flights = this.flightsSource.asObservable();

  constructor(private http: HttpClient) { }

  addFlight(model: any) {
    return this.http.post<Flight>(this.baseUrl + 'flights/add', model);
  }

  getFlights(model?: any) {
    let params = new HttpParams();

    if(model) {
      params = params.append('departureCityId', model.departureCityId.toString())
                    .append('arrivalCityId', model.arrivalCityId.toString())
                    .append('hasConnectingFlights', model.hasConnectingFlights.toString());
      
    }

    return this.http.get<Flight[]>(this.baseUrl + 'flights', {observe: 'response', params}).pipe(
      map(response => {
        return response.body;
      })
    );
  }

  deleteFlight(flightId: number) {
    return this.http.delete(this.baseUrl + 'flights/delete/' + flightId);
  }
}
