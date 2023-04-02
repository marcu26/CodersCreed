import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.prod';
import { JwtService } from '../jwt.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProjectsService {
  url = environment.URL + 'projects/';
  private header = new HttpHeaders({
    Authorization: 'Bearer ' + sessionStorage.getItem('Token')!,
  });

  constructor(private http: HttpClient, private jwt: JwtService) { }

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
}
