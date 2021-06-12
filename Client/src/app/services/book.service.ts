import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Book } from '../models/book';
import { BookForList } from '../models/bookForList';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class BookService {

  private createPath = environment.apiUrl + "/Management/CreateBook"
  private allPath = environment.apiUrl + "/Book/All"
  private dtailsPath = environment.apiUrl + "/Book"
  private editPath = environment.apiUrl + "/Management/UpdateBook"


  constructor(private http: HttpClient, private authService: AuthService) { }

  create(data: any): Observable<any>{
    return this.http.post<Book>(this.createPath,data)
  }

  all(): Observable<Array<BookForList>>{
    return this.http.get<Array<BookForList>>(this.allPath)
  }

  details(id: any): Observable<Book>{
    return this.http.get<Book>(this.dtailsPath + `/${id}`)
  }

  delete(id: any): Observable<any>{
    return this.http.delete(this.dtailsPath + `/${id}`)
  }

  update(data: any) : Observable<Book>{
    return this.http.put<Book>(this.editPath,data)
  }
}