import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  Router,
  RouterStateSnapshot,
  UrlTree,
} from '@angular/router';
import { Observable } from 'rxjs';
import { AccountService } from '../_services/account.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(private accountService: AccountService, private router: Router) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    const user = this.accountService.currentUser;

    if (user) {
      const role = user.role === 1 ? 'visitor' : user.role === 2 ? 'agent' : 'admin';
      const path = route.url[0].path;

      if (role === path)
        return true;
    }
    this.router.navigate(['/login']);
    return false;
  }
}
