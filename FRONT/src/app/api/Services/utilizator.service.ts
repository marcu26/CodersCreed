import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment.prod';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class UtilizatorService {
  header = new HttpHeaders({
    Authorization: 'apikey:' + localStorage.getItem('Token'),
  });

  constructor(private http: HttpClient) { }

  getUser(id: number) {
    return this.http.get<any>(
      environment.URL + '/Utilizator/GetUser?ID=' + id
    );
  }

  getAllUsers(): Observable<any[]> {
    return this.http.get<any[]>(
      environment.URL + '/Utilizator/GetAll');
  }

  activateAccount(code: string) {
    return this.http.get(
      environment.URL + '/Utilizator/Activate?Code=' + code);
  }


  put(utilizator: any) {
    return this.http.put<any>(
      environment.URL + '/Utilizator/Put',
      utilizator,
      { headers: this.header }
    );
  }




  delete(id: number) {
    return this.http.delete(environment.URL + '/Utilizator/Delete?ID=' + id, { responseType: 'text' });
  }
}
