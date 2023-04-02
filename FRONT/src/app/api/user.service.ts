import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.prod';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  url = environment.URL + 'users/';

  constructor(private http: HttpClient) { }

  authentificate_user(data: any): Observable<any> {
    const body = {
      email: data.email,
      password: data.password
    }
    return this.http.post(this.url + 'authenticate-user', body);
  }
}
