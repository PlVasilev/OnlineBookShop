import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import {  ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AllBooksComponent } from './all-books/all-books.component';




@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent,
    AllBooksComponent
    
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    HttpClientModule,
  ],
  exports:[
    [ LoginComponent,
      RegisterComponent
   ]
  ]
})
export class GeneralModule { }
