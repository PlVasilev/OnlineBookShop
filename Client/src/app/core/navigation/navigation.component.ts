import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthGuardService } from 'src/app/services/auth-guard.service';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {
  searchForm: FormGroup;

  constructor(private fb: FormBuilder,private router:Router,  private authServise: AuthService, private authGuard: AuthGuardService) { 
  this.searchForm = this.fb.group({
    'searchString': [''],
  })
}
  ngOnInit(): void {
  }
  get currentUser(){return this.authServise.isAutheticated()}

  get adminUser(){return this.authServise.isAdmin()}

  logoutHandler(){
    this.authGuard.isAdmin =false;
    this.authGuard.isLogged = false;
    this.authServise.logout();
  }

  get searchString() {
    return this.searchForm.get('searchString');
  }

  search(){
   let searchedText = this.searchForm.value['searchString']
   this.searchForm.reset()
   this.router.navigate([`/search/${searchedText}`])
  }
}
