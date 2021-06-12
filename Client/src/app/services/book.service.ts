import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Book } from '../models/book';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class BookService {

  private createPath = environment.apiUrl + "/Management/CreateBook"
  private minePath = environment.apiUrl + "/Contracts/Mine"
  private dtailsPath = environment.apiUrl + "/Contracts"
  private editPath = environment.apiUrl + "/Management/UpdateBook"


  constructor(private http: HttpClient, private authService: AuthService) { }

  create(data: any): Observable<any>{
    return this.http.post<Book>(this.createPath,data)
  }

  mine(): Observable<Array<Book>>{
    return this.http.get<Array<Book>>(this.minePath)
  }

  details(id: any): Observable<Book>{
    return this.http.get<Book>(this.dtailsPath + `/${id}`)
  }

  delete(id: any): Observable<any>{
    return this.http.delete(this.dtailsPath + `/${id}`)
  }

  edit(data: any) : Observable<Book>{
    return this.http.put<Book>(this.editPath,data)
  }
}