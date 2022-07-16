import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { City } from '../_models/city';

@Injectable({
  providedIn: 'root',
})
export class CityService {
  baseUrl: string = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getCities() {
    return this.http.get<City[]>(this.baseUrl + 'cities');
  }
}
