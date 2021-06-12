import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import {  ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AllBooksComponent } from './all-books/all-books.component';
import { DetailsBookComponent } from './details-book/details-book.component';




@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent,
    AllBooksComponent,
    DetailsBookComponent
    
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    HttpClientModule,
  ],
  exports:[
    [ LoginComponent,
      RegisterComponent,
      AllBooksComponent,
      DetailsBookComponent
   ]
  ]
})
export class GeneralModule { }
