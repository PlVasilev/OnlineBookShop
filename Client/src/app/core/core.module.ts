import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LandingComponent } from './landing/landing.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { NotAuthorizedComponent } from './not-authorized/not-authorized.component';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { FooterComponent } from './footer/footer.component';
import { NavigationComponent } from './navigation/navigation.component';




@NgModule({
  declarations: [
    LandingComponent, 
    NotFoundComponent, 
    NotAuthorizedComponent, 
    FooterComponent, 
    NavigationComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    FormsModule
  ],
  exports: [
    LandingComponent,
    NotFoundComponent,
    NotAuthorizedComponent,
    FooterComponent, 
    NavigationComponent]
})
export class CoreModule { }
