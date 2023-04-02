import { Injectable } from '@angular/core';
import jwt_decode from 'jwt-decode';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root',
})
export class JwtService {
  constructor(private cookies: CookieService) { }

  public getJwt() {
    return this.cookies.get("token");
  }

  public decodeToken(): any {
    try {
      const jwt = this.cookies.get("token");
      return jwt_decode(jwt!);
    } catch (Error) {
      return null;
    }
  }
}
