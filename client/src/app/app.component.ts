import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';
import { LiveReloadService } from './_services/live-reload.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'flight booking';

  constructor(
    private accountService: AccountService,
    private router: Router,
    private liveReloadService: LiveReloadService
  ) {}

  ngOnInit() {
    this.setCurrentUser();
  }

  setCurrentUser() {
    const storedUser = localStorage.getItem('user');

    if (storedUser) {
      const user: User = JSON.parse(storedUser);
      this.accountService.setCurrentUser(user);

      this.liveReloadService.createHubConnection();

      const currentUserRole: number = this.accountService.getCurrentUserRole();
      switch (currentUserRole) {
        case 1:
          this.router.navigate(['/visitor']);
          break;
        case 2:
          this.router.navigate(['/agent']);
          break;
        case 3:
          this.router.navigate(['/admin']);
          break;
        default:
          break;
      }
    }
  }
}
