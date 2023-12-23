/* eslint-disable @typescript-eslint/no-inferrable-types */
/* eslint-disable @typescript-eslint/no-explicit-any */
import { HttpClient, HttpHeaders} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LearningservicesService {

  MFSfinalUri : any;
  successfulURI : any;

     
 uniqueProductKey='';
 uniquePlanKey='';
 authToken='';
  private _headers!: HttpHeaders;
  // private distheaders!: HttpHeaders;
  private _headerslogin!: HttpHeaders;
  token = localStorage.getItem('BToken');
  // eslint-disable-next-line @typescript-eslint/no-inferrable-types
  baseUrl: string ="https://localhost:7073/api/v1.1/";
 // baseUrl: string ="https://tmsenterprisedevapi.azurewebsites.net/api/v1.1/";
  constructor(private http:HttpClient, private router: Router,  private httpClient: HttpClient) { 
    this.setLoginSecurityHeaders();
    this.setSecurityHeaders();

  }
  setSecurityHeaders() {
    this._headers = new HttpHeaders();
    this._headers = this._headers.append("Content-type", "application/json");
    this._headers = this._headers.append("Accept", "application/json");
    this._headers = this._headers.append("Access-Control-Allow-Methods", "POST, GET, OPTIONS, PUT");
    // this._headers = this._headers.append("X-XSS-Protection", "1; mode=block");
    //this._headers = this._headers.append("Content-Security-Policy", "default-src 'self' https://disbursemenportal.azurewebsites.net; script-src 'self' https://disbursemenportal.azurewebsites.net; object-src 'none';");
    // this._headers = this._headers.append("Referrer-Policy", "no-referrer");
    // this._headers = this._headers.append("X-Content-Type-Options", "nosniff");
    // this._headers = this._headers.append("Permission-Policy", "geolocation=()");
    // this._headers = this._headers.append("Permission-Policy", "geolocation=(), camera=(), microphone=()");
    // this._headers = this._headers.append("Strict-Transport-Security", "max-age=31536000; includeSubDomains");
    // this._headers = this._headers.append("X-Frame-Options", "SAMEORIGIN");
    this._headers = this._headers.append("Authorization", "Bearer " + localStorage.getItem('BToken'));
  }
  
  setLoginSecurityHeaders() {

    
    this._headerslogin = new HttpHeaders();
    this._headerslogin = this._headerslogin.append("Content-type", "application/json");
    this._headerslogin = this._headerslogin.append("Accept", "application/json");
   // this._headerslogin = this._headerslogin.append('Access-Control-Allow-Origin','*');
    this._headerslogin = this._headerslogin.append("Access-Control-Allow-Methods", "POST, GET, OPTIONS, PUT");
    // this._headerslogin = this._headerslogin.append("X-XSS-Protection", "1; mode=block");
    // this._headerslogin = this._headerslogin.append("Referrer-Policy", "no-referrer");
    // this._headerslogin = this._headerslogin.append("X-Content-Type-Options", "nosniff");
    //this._headerslogin = this._headerslogin.append("Permission-Policy", "geolocation=()");
    // this._headers = this._headers.append("Permission-Policy", "geolocation=(), camera=(), microphone=()");
    //this._headerslogin = this._headerslogin.append("Strict-Transport-Security", "max-age=31536000; includeSubDomains");
   // this._headerslogin = this._headerslogin.append("X-Frame-Options", "SAMEORIGIN");
  }
  loginUser(reqData: any): Observable<any> {
    this._headers = new HttpHeaders();
    this._headers = this._headers.append("Content-type", "application/json");
    this._headers = this._headers.append("Accept", "application/json");
    this._headers = this._headers.append("Access-Control-Allow-Methods", "POST, GET, OPTIONS, PUT");
    this._headers = this._headers.append("Authorization", "Bearer " + localStorage.getItem('BToken'));
    return this.http.post( this.baseUrl + "login/logIn",reqData, { headers:this._headers });
   }
   getAllUser(): Observable<any> {
    this._headers = new HttpHeaders();
    this._headers = this._headers.append("Content-type", "application/json");
    this._headers = this._headers.append("Accept", "application/json");
    this._headers = this._headers.append("Access-Control-Allow-Methods", "POST, GET, OPTIONS, PUT");
    this._headers = this._headers.append("Authorization", "Bearer " + localStorage.getItem('BToken'));
    return this.http.post( this.baseUrl + "AppUser/getallusers", { headers:this._headers });
   }
}
