import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Book } from '../models/book';
import { BookForInventory } from '../models/bookForInventory';
import { BookForList } from '../models/bookForList';

@Injectable({
  providedIn: 'root'
})
export class BookService {

  private createPath = environment.apiUrl + "/Management/CreateBook";
  private allPath = environment.apiUrl + "/Book/All";
  private dtailsPath = environment.apiUrl + "/Book";
  private deletePath = environment.apiUrl + "/Management/DeleteBook";
  private editPath = environment.apiUrl + "/Management/UpdateBook";
  private inventoryPath = environment.apiUrl + "/Management/Inventory";


  constructor(private http: HttpClient) { }

  create(data: any): Observable<any>{
    return this.http.post<Book>(this.createPath,data);
  }

  all(): Observable<Array<BookForList>>{
    return this.http.get<Array<BookForList>>(this.allPath);
  }

  details(id: any): Observable<Book>{
    return this.http.get<Book>(this.dtailsPath + `/${id}`);
  }

  delete(id: any): Observable<any>{
    return this.http.delete(this.deletePath + `/${id}`);
  }

  update(data: any) : Observable<Book>{
    return this.http.put<Book>(this.editPath,data);
  }

  inventory(): Observable<Array<BookForInventory>>{
    return this.http.get<Array<BookForInventory>>(this.inventoryPath);
  }
}