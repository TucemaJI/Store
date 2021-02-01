import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Email, HttpService } from '../../services/HttpService';

@Component({
  selector: 'app-password-recovery',
  templateUrl: './password-recovery.component.html',
  styleUrls: ['./password-recovery.component.css']
})
export class PasswordRecoveryComponent implements OnInit {
  emailForm: FormGroup;

  constructor(private httpService: HttpService) { }

  ngOnInit(): void {
    this.emailForm = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email])
    });
  }

  passwordSent: boolean;



  public hasError = (controlName: string, errorName: string) => {
    return this.emailForm.controls[controlName].hasError(errorName);
  }

  public continue(email: Email) {
    if (this.emailForm.valid) {
      debugger;
      this.passwordSent = true;
      this.httpService.postEmail(email)
      .subscribe(
        () => { this.passwordSent = true;debugger;},
        error => console.log(error)
      );
    }
  }

}
