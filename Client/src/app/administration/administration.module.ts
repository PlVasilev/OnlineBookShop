import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateBookComponent } from './create-book/create-book.component';
import { UpdateBookComponent } from './update-book/update-book.component';
import { ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    CreateBookComponent,
    UpdateBookComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],exports:[
    CreateBookComponent,
    UpdateBookComponent
  ]
})
export class AdministrationModule { }
