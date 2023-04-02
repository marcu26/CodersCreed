import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.prod';
import { JwtService } from '../jwt.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LeaderboardService {
  url = environment.URL + 'users/';
  private header = new HttpHeaders({
    Authorization: 'Bearer ' + sessionStorage.getItem('Token')!,
  });

  constructor(private http: HttpClient, private jwt: JwtService) { }

  get_leaderboard(start: number, length: number): Observable<any> {
    const body = {
      draw: 0,
      start: start,
      length: length,
    }
    return this.http.post(this.url + 'get-leaderboard', body, { headers: this.header })
  }
}
