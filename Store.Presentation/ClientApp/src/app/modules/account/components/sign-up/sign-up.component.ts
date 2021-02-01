import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormGroupDirective, NgForm, Validators } from '@angular/forms';
import { CheckerErrors, MyErrorStateMatcher } from 'src/app/modules/shared/validator';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})

export class SignUpComponent implements OnInit {

  signUpForm: FormGroup;

  matcher = new MyErrorStateMatcher();

  constructor() { }

  ngOnInit(): void {
    this.signUpForm = new FormGroup({
      firstName: new FormControl('', [Validators.required]),
      lastName: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required, CheckerErrors.checkUpperCase, CheckerErrors.checkSpecialSymbol,
      CheckerErrors.checkLength, CheckerErrors.checkNum]),
      confirmPassword: new FormControl('', [Validators.required]),
    }, { validators: CheckerErrors.checkPasswords });
  }
  
  public hasError = (controlName: string, errorName: string) => {
    return this.signUpForm.controls[controlName].hasError(errorName);
  }
}

