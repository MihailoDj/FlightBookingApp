import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-user-controls',
  templateUrl: './user-controls.component.html',
  styleUrls: ['./user-controls.component.css']
})
export class UserControlsComponent implements OnInit {
  currentUser: string;

  constructor(private accountService: AccountService, private router: Router) { }

  ngOnInit(): void {
    this.currentUser = this.accountService.getCurrentUser().userName;
  }

  logout() {
    this.accountService.logout();
    this.router.navigate(['login']);
  }

}
