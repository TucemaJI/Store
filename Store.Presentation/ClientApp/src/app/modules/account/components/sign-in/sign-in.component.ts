import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Store } from '@ngxs/store';
import { ILogin } from '../../../shared/models/ILogin.model';
import { SignIn } from '../../store/account.actions';
import { DOCUMENT } from '@angular/common';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit {
  public userForm: FormGroup;
  public remember: boolean = false;

  constructor(@Inject(DOCUMENT) private document: Document, private store: Store) { }

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

  public onSignIn(googleUser) {
    debugger;
    var profile = googleUser.getBasicProfile();
    console.log('ID: ' + profile.getId()); // Do not send to your backend! Use an ID token instead.
    console.log('Name: ' + profile.getName());
    console.log('Image URL: ' + profile.getImageUrl());
    console.log('Email: ' + profile.getEmail()); // This is null if the 'email' scope is not present.
  }

}
