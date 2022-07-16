import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Reservation } from '../_models/reservation';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root',
})
export class ReservationService {
  baseUrl: string = environment.apiUrl;

  constructor(private http: HttpClient, private accountService: AccountService) {}

  addReservation(model: any) {
    return this.http.post(this.baseUrl + 'reservations', model);
  }

  getReservations() {
    return this.http.get<Reservation[]>(
      this.baseUrl + 'reservations'
    );
  }

  getReservationsForUser() {
    const currentUserId = this.accountService.currentUser.id;

    return this.http.get<Reservation[]>(this.baseUrl + 'reservations/user/' + currentUserId);
  }

  updateReservation(model: any) {
    return this.http.put(
      this.baseUrl + `reservations/update`,
      model
    );
  }

  deleteReservation(flightId: number, userId: number) {
    return this.http.delete(
      this.baseUrl + `reservations/delete/${flightId}/${userId}`
    );
  }
}
