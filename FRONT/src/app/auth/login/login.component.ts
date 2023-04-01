import { Component, HostListener } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { JwtService } from 'src/app/jwt.service';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  public formGroup_login: FormGroup;


  constructor(private auth: AuthService, private router: Router, private jwtService: JwtService) {
    this.formGroup_login = new FormGroup({
      email: new FormControl('', [Validators.required]),
      password: new FormControl('', [Validators.required]),
    });
  }

  try_login() {
    this.auth.login(this.formGroup_login.value).subscribe({
      next: (response) => {
        if (response.Token == undefined) {
          alert(response.Message);
          return;
        }
        sessionStorage.setItem("Token", response.Token);
        this.router.navigate(['library'])
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
