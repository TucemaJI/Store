import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import { IUser } from 'src/app/modules/shared/models/IUser.model';
import { CheckerErrors, MyErrorStateMatcher as ErrorStateMatcher } from 'src/app/modules/shared/validator';
import { IAppState } from 'src/app/store/state/app.state';
import { signUp } from '../../store/account.actions';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})

export class SignUpComponent implements OnInit {

  public signUpForm: FormGroup;

  public matcher = new ErrorStateMatcher();

  constructor(private store: Store<IAppState>) { }

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
  
  public hasError = (controlName: string, errorName: string): boolean => {
    return this.signUpForm.controls[controlName].hasError(errorName);
  }

  public submit(user: IUser) {
    this.store.dispatch(signUp({user}));
  }
}

