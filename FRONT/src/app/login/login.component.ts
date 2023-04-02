import { Component, HostListener } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { JwtService } from '../jwt.service';
import { UserService } from '../api/user.service';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  public formGroup_login: FormGroup;

  constructor(private userService: UserService, private router: Router, private cookies: CookieService) {
    this.formGroup_login = new FormGroup({
      email: new FormControl('', [Validators.required]),
      password: new FormControl('', [Validators.required]),
    });
  }

  try_login() {
    this.userService.authentificate_user(this.formGroup_login.value).subscribe({
      next: (response) => {
        console.log(response);
        if (response.token == undefined) {
          alert(response.Message)
          return;
        }
        this.cookies.set('token', response.token);
        this.router.navigate(['projects'])
      },
      error: (error) => {
        alert("Email or password wrong!")
      }
    })
  }

  @HostListener('document:keyup', ['$event'])
  handleKeyboardEvent(event: KeyboardEvent) {
    if (event.key == 'Enter') {
      this.try_login();
    }
  }
}
