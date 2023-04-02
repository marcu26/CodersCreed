import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.prod';
import { JwtService } from '../jwt.service';
import { Observable } from 'rxjs';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class TasksService {
  url_task = environment.URL + 'tasks/';
  url_courses = environment.URL + 'course/';
  private header = new HttpHeaders({
    Authorization: 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiIxIiwiVXNlcm5hbWUiOiJhZG1pbiIsInJvbGVzIjpbIkFkbWluaXN0cmF0b3IiLCJNYW5hZ2VyIiwiQWNjb3VudENyZWF0b3IiLCJVc2VyIl0sIm5iZiI6MTY4MDM4OTE0NSwiZXhwIjoxNjg4MjUxNTQ1LCJpYXQiOjE2ODAzODkxNDUsImlzcyI6IkhhY2thdG9uQmFja2VuZCIsImF1ZCI6IkhhY2thdG9uQmFja2VuZCJ9.OfZcf6DxVF4IdeH3MXosR1csNbb8D12UGoJ8VgY2cI4',
  });

  constructor(private http: HttpClient, private cookies: CookieService) { }

  get_pagina_task(start: number, length: number): Observable<any> {
    const body = {
      draw: 0,
      start: start,
      length: length,
      projectId: null,
      userId: null,
      beforeDeadline: false,
      afterDeadline: false,
    }
    return this.http.post(this.url_task + 'get-pagina', body, { headers: this.header })
  }

  get_pagina_courses(start: number, length: number): Observable<any> {
    const body = {
      draw: 0,
      start: start,
      length: length,
    }
    return this.http.post(this.url_courses + 'get-pagina', body, { headers: this.header })
  }

}
