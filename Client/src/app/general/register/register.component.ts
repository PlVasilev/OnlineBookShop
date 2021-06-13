import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/services/auth.service';
import { MustMatch } from 'src/app/services/validators/passwordValidator';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup
  constructor(private fb: FormBuilder, private authService: AuthService,private toastrService: ToastrService, private router: Router) {
    this.registerForm = this.fb.group({})
   }

  ngOnInit(): void {
    this.registerForm = this.fb.group({
      'username': ['',Validators.required],
      'email': ['',Validators.required],
      'password': ['',Validators.required],
      'confirmPassword': ['',Validators.required]
    }, {
      validator: MustMatch('password', 'confirmPassword')
  })
  }

  register(){
    console.log(this.registerForm.value);
    this.authService.register(this.registerForm.value).subscribe(data =>{
      this.toastrService.success("success", "You have Registered!");
        this.router.navigate(["login"])
    })
    
  }

  get username() {
    return this.registerForm.get('username')
  }

  get email() {
    return this.registerForm.get('email')
  }

  get password() {
    return this.registerForm.get('password')
  }

  get confirmPassword() {
    return this.registerForm.get('confirmPassword')
  }

}
