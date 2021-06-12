import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService {
  isLogged = false;
  isAdmin = false;

  constructor(private authService: AuthService, private router: Router) { }
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
    if (this.authService.isAutheticated()) {
      this.isLogged = true;
      if (this.authService.isAdmin()) {
        this.isAdmin = true;
      }
    }

    if (this.isLogged === route.data.isLogged) {
      return true;
    }
    else if (this.isAdmin === route.data.isAdmin) {
      return true;
    }
    else {
      if(this.isLogged == true){
        console.log(this.isLogged);
        this.router.navigate(['/register'])
        return false;
      }
      this.router.navigate(['/login'])
      return false;
    }
  }
}
