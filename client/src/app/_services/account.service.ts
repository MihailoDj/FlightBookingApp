import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';
import { LiveReloadService } from './live-reload.service';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  baseUrl: string = environment.apiUrl;
  currentUser: User | null = null;

  constructor(private http: HttpClient, private liveReloadService: LiveReloadService) {}

  login(model: any) {
    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe(
      map((response: User) => {
        const user: User = response;
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUser = user;

          this.liveReloadService.createHubConnection();
        }

        return user;
      })
    );
  }

  registerUser(model: any) {
    return this.http.post<User>(this.baseUrl + 'account/register-user', model).pipe(
      map((response: User) => {
        const user: User = response;
        return user;
      })
    );
  }

  getCurrentUserRole(): number {
    if (this.currentUser)
      return this.currentUser.role;
    return -1;
  }

  setCurrentUser(user: User) {
    this.currentUser = user;
  }

  getCurrentUser() {
    return this.currentUser;
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUser = null;

    this.liveReloadService.stopHubConnection();
  }
}
