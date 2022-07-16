import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  model: any = {};
  errorMessage: string;
  @ViewChild('loginForm') loginForm: NgForm;

  constructor(private accountService: AccountService, private router: Router) { }

  ngOnInit(): void {
  }

  login() {
    if (this.loginForm.invalid){
      this.errorMessage = 'Invalid input.';
      return;
    }

    this.accountService.login(this.model).subscribe(response => {
      
      const currentUserRole: number = this.accountService.getCurrentUserRole();
      switch(currentUserRole) {
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
    }, error => {
      this.errorMessage = error.error.title;
    });
  }
}
