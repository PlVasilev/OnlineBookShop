import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private loginPath = environment.apiUrl + "/identity/login"
  private registerPath = environment.apiUrl + "/identity/register"
  
    constructor(private http: HttpClient, private toastrService: ToastrService, private router: Router) { }
  
    login(data: any): Observable<any>{
      return this.http.post(this.loginPath,data)
    }
  
    register(data: any): Observable<any>{
      return this.http.post(this.registerPath,data)
    }
  
    saveToken(token: string){
      localStorage.setItem('token', token)
    }
  
    getToken(){
      let token  = localStorage.getItem('token');
      if(token == null){
       return "";
      }
      return token;
    }
  
    isAdmin(){
      let token = this.getToken();
      if (token !== ""){
        let roles = JSON.parse(window.atob(token.split('.')[1])).role;
        return roles.includes('Admin');
      }
      return false;
    }
  
    isAutheticated(){
      if(this.getToken()){
        return true;
      }
      return false;
    }

    logout() {
      try {
       localStorage.clear();
       this.toastrService.success("success", "You have logged out!");
        console.log( this.getToken()) ;
       this.isAdmin();
       this.router.navigate(['/']);
      } catch (error) {
        this.toastrService.error("error", "There was problem with your logging you out!");
        console.log(error);
      }
    }
  }
