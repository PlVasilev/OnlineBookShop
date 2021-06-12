import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateBookComponent } from './create-book/create-book.component';
import { UpdateBookComponent } from './update-book/update-book.component';



@NgModule({
  declarations: [
    CreateBookComponent,
    UpdateBookComponent
  ],
  imports: [
    CommonModule
  ],exports:[
    CreateBookComponent,
    UpdateBookComponent
  ]
})
export class AdministrationModule { }
