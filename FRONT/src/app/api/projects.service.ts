import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.prod';
import { JwtService } from '../jwt.service';
import { Observable } from 'rxjs';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class ProjectsService {
  url = environment.URL + 'projects/';
  private header = new HttpHeaders({
    Authorization: 'Bearer ' + this.cookies.get('token'),
  });

  constructor(private http: HttpClient, private cookies: CookieService, private jwt: JwtService) { }

  get_pagina(start: number, length: number): Observable<any> {
    const body = {
      draw: 0,
      start: start,
      length: length,
      name: null,
      userId: this.jwt.decodeToken()['UserId'],
    }
    return this.http.post(this.url + 'get-pagina', body, { headers: this.header })
  }

  send_image(image: any): Observable<any> {

    console.log(image)
    const objSend = {
      'data': image.imageAsDataUrl.split(',')[1],
      'type': 'jpeg'
    }
    return this.http.post(this.url + 'predict', objSend, {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + this.cookies.get('token'),
      })
    });

  }
}
