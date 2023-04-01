import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.prod';

@Injectable({
  providedIn: 'root'
})
export class TipPublicatieService {
  private header = new HttpHeaders({
    // Authorization: sessionStorage.getItem('Token')!,
  });
  constructor(private http: HttpClient) { }

  getAll(): Observable<any[]> {
    return this.http.get<any[]>(environment.URL + "/TipPublicatie/GetAll", { headers: this.header });
  }

  post(tipPublicatie: any) {
    return this.http.post<any>(environment.URL + "/TipPublicatie/Post", tipPublicatie);
  }

  put(tipPublicatie: any) {
    return this.http.put<any>(environment.URL + "/TipPublicatie/Put", tipPublicatie);
  }

  delete(id: number) {
    return this.http.delete(environment.URL + "/TipPublicatie/Delete?ID=" + id, { responseType: 'text' });
  }
}
