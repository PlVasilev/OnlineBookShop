import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import {  ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AllBooksComponent } from './all-books/all-books.component';
import { DetailsBookComponent } from './details-book/details-book.component';
import { RouterModule } from '@angular/router';
import { SearchBooksComponent } from './search-books/search-books.component';
import { CartComponent } from './cart/cart.component';




@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent,
    AllBooksComponent,
    DetailsBookComponent,
    SearchBooksComponent,
    CartComponent
    
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    HttpClientModule,
    RouterModule
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
