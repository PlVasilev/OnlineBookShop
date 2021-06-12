import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-landing',
  templateUrl: './landing.component.html',
  styleUrls: ['./landing.component.css']
})
export class LandingComponent implements OnInit {

  constructor(private authServise: AuthService) { }

  ngOnInit(): void {
  }
  get currentUser(){return this.authServise.isAutheticated()}
}
