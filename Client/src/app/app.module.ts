import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CoreModule } from './core/core.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { AuthService } from './services/auth.service';
import { BookService } from './services/book.service';
import { CartService } from './services/cart.service';
import { AuthGuardService } from './services/auth-guard.service';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { TokenInterceptor } from './services/token-interceptor.service';
import { ErrorInterceptorService } from './services/error-interceptor.service';
import { AdministrationModule } from './administration/administration.module';
import { GeneralModule } from './general/general.module';
import { QuantityLimitDirective } from './services/directives/quantity-limit.directive';


@NgModule({
  declarations: [
    AppComponent,
    QuantityLimitDirective
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    CoreModule,
    AdministrationModule,
    GeneralModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot()
  ],
  providers: [   
    AuthService,
    BookService,
    CartService,
    AuthGuardService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    }, {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptorService,
      multi: true
    }],
  bootstrap: [AppComponent]
})
export class AppModule { }
