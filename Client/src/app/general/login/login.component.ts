import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;

  constructor(private fb: FormBuilder, private authService: AuthService, private toastrService: ToastrService, private router: Router) { 
    this.loginForm = this.fb.group({
      'username': ['',[Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      'password': ['',[Validators.required, Validators.minLength(3), Validators.maxLength(100)]]
    })
  }

  ngOnInit(): void {
  }

  login(){
    this.authService.login(this.loginForm.value).subscribe( data =>{
      this.authService.saveToken(data['token']);
      this.toastrService.success("You have Logged In!");
      this.router.navigate(["books"])
    })
  }

  get username() {
    return this.loginForm.get('username');
  }

  get password() {
    return this.loginForm.get('password');
  }
}
