import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { JwtService } from 'src/app/jwt.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent {
  firstFormGroup: FormGroup;
  secondFormGroup: FormGroup;
  isLinear = true;

  oldValues: any;
  userID: any;

  constructor(
    private jwtService: JwtService) {
    this.firstFormGroup = new FormGroup({
      password: new FormControl(''),
      email: new FormControl(''),
      firstName: new FormControl(''),
      lastName: new FormControl(''),
      institution: new FormControl(''),
      uefid: new FormControl(''),
    });

    this.secondFormGroup = new FormGroup({
      password: new FormControl('', [Validators.required])
    })

    let data = jwtService.decodeToken();
    this.userID = data['userID'];

  }

  sendData() {
  }

  onSubmit() {
  }
}
