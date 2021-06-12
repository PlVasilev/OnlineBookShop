import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Observable, throwError } from 'rxjs';
import {retry,catchError} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ErrorInterceptorService implements HttpInterceptor {
  constructor(private toastrService: ToastrService) {}
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
   return next.handle(request).pipe(
     retry(1),
     catchError(err => {
      let message = "";
      if (err.status === 401) {
        message = "Invalid Credentials (Token, Username or Password)",
        console.log(message);
      }
      else if (err.status === 404) {
        message = "Item Not Found",
        console.log(message);
      } else if (err.status === 400) {
        message = "Bad Request",
        console.log(message);
      } else {
        message = "Unexpected ERROR"
      }
      this.toastrService.error(message)
      return throwError(err)
     })
   );
  }
}
