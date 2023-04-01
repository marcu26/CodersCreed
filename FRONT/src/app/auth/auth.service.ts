import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.prod';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private http: HttpClient) { }


  login(data: any) {
    let login = {
      email: data.email,
      parola: data.password
    }

    return this.http.post<any>(
      environment.URL + '/Utilizator/Login',
      login);
  }

  register(data: any) {
    if (data.institution.nume == undefined)
      data.institution = {
        nume: data.institution,
        id: 0
      }

    let register = {
      parola: data.password,
      email: data.email,
      prenume: data.firstName,
      nume: data.lastName,
      numeInstitutie: data.institution.nume,
      idInstitutie: data.institution.id,
      uefid: data.uefid,
      drepturi: 2,
      activated: 'false'
    }
    console.log(register);


    return this.http.post<any>(
      environment.URL + '/Utilizator/Register',
      register);
  }

  generateCode(data: any) {
    let email = { data: data }
    return this.http.post<any>(
      environment.URL + '/Utilizator/GenerateCode',
      email);
  }

  verifyCode(id: any, code: any) {
    return this.http.get(
      environment.URL + '/Utilizator/VerifyCode?ID=' + id + '&Code=' + code,
    );
  }

  updatePassword(data: any) {
    return this.http.put(
      environment.URL + '/Utilizator/ChangePassword',
      data
    );
  }

  getInstitutions(): Observable<any[]> {
    return this.http.get<any>(
      environment.URL + '/Utilizator/GetInstiutions'
    )
  }

}
