import { Component, OnInit } from '@angular/core';
import { AuthGuardService } from 'src/app/services/auth-guard.service';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {

  constructor(private authServise: AuthService, private authGuard: AuthGuardService) { }

  ngOnInit(): void {
  }
  get currentUser(){return this.authServise.isAutheticated()}

  get adminUser(){return this.authServise.isAdmin()}

  logoutHandler(){
    this.authGuard.isAdmin =false;
    this.authGuard.isLogged = false;
    this.authServise.logout();
  }
}
