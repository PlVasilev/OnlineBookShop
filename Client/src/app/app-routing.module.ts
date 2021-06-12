import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LandingComponent } from './core/landing/landing.component';
import { NotAuthorizedComponent } from './core/not-authorized/not-authorized.component';
import { NotFoundComponent } from './core/not-found/not-found.component';
import { LoginComponent } from './general/login/login.component';
import { RegisterComponent } from './general/register/register.component';
import { AuthGuardService } from './services/auth-guard.service';

const routes: Routes = [  {
  path: '',
  pathMatch: 'full',
  component: LandingComponent
},
{
  path: 'login',
  component: LoginComponent,
  canActivate: [AuthGuardService],
  data: {
      isLogged: false
  }
},
{
  path: 'register',
  component: RegisterComponent,
  canActivate: [AuthGuardService],
  data: {
      isLogged: false
  }
},
{
  path: 'notauthorized',
  component: NotAuthorizedComponent
},
{
  path: '**',
  component: NotFoundComponent
}];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
