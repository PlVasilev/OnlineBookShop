import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { BookForCart } from '../models/bookForCart';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  private addToCartPath = environment.apiUrl + "/Cart/AddToCart";
  private getCartPath = environment.apiUrl + "/Cart/GetCart";

  constructor(private http: HttpClient) { }

  addToCart(data: any): Observable<any>{
    return this.http.post<string>(this.addToCartPath,data);
  }

  getCart(): Observable<Array<BookForCart>>{
    return this.http.get<Array<BookForCart>>(this.getCartPath);
  }
}
