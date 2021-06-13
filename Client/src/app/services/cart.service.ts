import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  private addToCartPath = environment.apiUrl + "/Cart/AddToCart";

  constructor(private http: HttpClient) { }

  addToCart(data: any): Observable<any>{
    return this.http.post<string>(this.addToCartPath,data);
  }
}
