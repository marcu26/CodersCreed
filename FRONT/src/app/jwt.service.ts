import { Injectable } from '@angular/core';
import jwt_decode from 'jwt-decode';

@Injectable({
  providedIn: 'root',
})
export class JwtService {
  constructor() { }

  public decodeToken(): any {
    try {
      const jwt = sessionStorage.getItem("Token");
      return jwt_decode(jwt!);
    } catch (Error) {
      return null;
    }
  }
}
