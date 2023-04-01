import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatStepper } from '@angular/material/stepper';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent {
  emailFormGroup: FormGroup;
  codeFormGroup: FormGroup;
  newPasswordFormGroup: FormGroup;
  private id: number;


  constructor(private auth: AuthService, private router: Router) {
    this.id = 0;
    this.emailFormGroup = new FormGroup({ email: new FormControl('', [Validators.required]) })
    this.codeFormGroup = new FormGroup({ code: new FormControl('', [Validators.required]) })
    this.newPasswordFormGroup = new FormGroup({ password: new FormControl('', [Validators.required]) })
  }

  onSubmitEmail(stepper: MatStepper) {
    this.auth.generateCode(this.emailFormGroup.value.email).subscribe(res => {
      console.log(res);
      this.id = res['ID']
      stepper.selected!.completed = true;
      stepper.next();
    },
      (err: HttpErrorResponse) => {
        alert(err.error);
      });
    return true;
  }
  onSubmitCode(stepper: MatStepper) {
    this.auth.verifyCode(this.id, this.codeFormGroup.value.code).subscribe(res => {
      console.log(res);
      stepper.selected!.completed = true;
      stepper.next();
    },
      (err: HttpErrorResponse) => {
        alert(err.error);
      });
    return true;
  }
  onSubmitPassword() {
    console.log(this.newPasswordFormGroup)
    const recoverWrapper = {
      id: this.id,
      password: this.newPasswordFormGroup.value.password,
      code: this.codeFormGroup.value.code
    };
    this.auth.updatePassword(recoverWrapper).subscribe(res => {
      console.log(res);
      alert(res);
      this.router.navigate(['/login'])
    },
      (err: HttpErrorResponse) => {
        alert(err.error);
      });
    return true;
  }
}
