import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Store } from '@ngxs/store';
import { ILogin } from '../../../shared/models/ILogin.model';
import { SignIn } from '../../store/account.actions';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit {
  public userForm: FormGroup;
  public remember: boolean = false;

  constructor(private store: Store) { }

  ngOnInit(): void {
    this.userForm = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required]),
    });
  }

  public hasError = (controlName: string, errorName: string): boolean => {
    return this.userForm.controls[controlName].hasError(errorName);
  }

  public submit(userFormValue: ILogin): void {
    const model: ILogin = {
      email: userFormValue.email,
      password: userFormValue.password,
    }
    this.store.dispatch(new SignIn(model, this.remember));
  }

}
