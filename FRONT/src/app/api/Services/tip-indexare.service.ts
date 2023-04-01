import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.prod';

@Injectable({
  providedIn: 'root'
})
export class TipIndexareService {

  constructor(private http: HttpClient) { }

  getAll(): Observable<any[]> {
    return this.http.get<any[]>(environment.URL + "/TipIndexare/GetAll");
  }

  post(tipIndexare: any) {
    return this.http.post<any>(environment.URL + "/TipIndexare/Post", tipIndexare);
  }

  put(tipIndexare: any) {
    return this.http.put<any>(environment.URL + "/TipIndexare/Put", tipIndexare);
  }

  delete(id: number) {
    return this.http.delete(environment.URL + "/TipIndexare/Delete?ID=" + id, { responseType: 'text' });
  }
}

