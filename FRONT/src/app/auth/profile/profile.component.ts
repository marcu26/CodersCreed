import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UtilizatorService } from 'src/app/api/Services/utilizator.service';
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

  constructor(private utilizatorService: UtilizatorService,
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


    utilizatorService.getUser(this.userID).subscribe((res) => {
      console.log(res)

      this.oldValues = {};

      this.oldValues.email = res["email"];
      this.oldValues.id = this.userID;
      this.oldValues.institutie = res["institutie"];
      this.oldValues.nume = res["nume"];
      this.oldValues.parola = "************************";
      this.oldValues.prenume = res["prenume"];
      this.oldValues.uefid = res["uefid"];

      this.firstFormGroup.patchValue({
        password: "************************",
        email: res["email"],
        firstName: res["prenume"],
        lastName: res["nume"],
        institution: res["institutie"],
        uefid: res["uefid"],
      })
    });
  }

  sendData() {

    const utilizator = {
      id: this.userID,
      numeUtilizator: this.firstFormGroup.value.username,
      parola: this.firstFormGroup.value.password,
      drepturi: this.firstFormGroup.value.username,
      email: this.firstFormGroup.value.email,
      prenume: this.firstFormGroup.value.firstName,
      nume: this.firstFormGroup.value.lastName,
      institutie: this.firstFormGroup.value.username,
      uefid: this.firstFormGroup.value.username,
      activated: this.firstFormGroup.value.username
    };

    this.utilizatorService.put(utilizator).subscribe(
      (res) => { },
      (err: HttpErrorResponse) => { }
    );
  }

  onSubmit() {
  }
}
