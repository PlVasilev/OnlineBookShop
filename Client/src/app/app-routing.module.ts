import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateBookComponent } from './administration/create-book/create-book.component';
import { UpdateBookComponent } from './administration/update-book/update-book.component';
import { LandingComponent } from './core/landing/landing.component';
import { NotAuthorizedComponent } from './core/not-authorized/not-authorized.component';
import { NotFoundComponent } from './core/not-found/not-found.component';
import { AllBooksComponent } from './general/all-books/all-books.component';
import { DetailsBookComponent } from './general/details-book/details-book.component';
import { LoginComponent } from './general/login/login.component';
import { RegisterComponent } from './general/register/register.component';
import { SearchBooksComponent } from './general/search-books/search-books.component';
import { AuthGuardService } from './services/auth-guard.service';

const routes: Routes = [{
  path: '',
  pathMatch: 'full',
  component: LandingComponent
},
{
  path: 'login',
  component: LoginComponent,
  canActivate: [AuthGuardService],
  data: { isLogged: false }
},
{
  path: 'register',
  component: RegisterComponent,
  canActivate: [AuthGuardService],
  data: { isLogged: false }
},
{
  path: 'books',
  component: AllBooksComponent,
  canActivate: [AuthGuardService],
  data: { isLogged: true }
},
{
  path: "books/:id",
  component: DetailsBookComponent,
  canActivate: [AuthGuardService],
  data: { isLogged: true }
},
{
  path: "search/:search",
  component: SearchBooksComponent,
  canActivate: [AuthGuardService],
  data: { isLogged: true }
},
{
  path: 'createBook',
  component: CreateBookComponent,
  canActivate: [AuthGuardService],
  data: { isAdmin: true }
},
{
  path: ':id/update',
  component: UpdateBookComponent,
  canActivate: [AuthGuardService],
  data: { isAdmin: true}
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
