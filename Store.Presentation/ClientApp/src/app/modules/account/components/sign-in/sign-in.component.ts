import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { IAppState } from 'src/app/store/state/app.state';
import { ILoginModel } from '../../models/ILoginModel';
import { HttpService} from '../../services/HttpService';
import { EAccountActions, signIn } from '../../store/account.actions';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit {
  userForm: FormGroup;

  done: boolean = false;

  constructor(private store: Store<IAppState>) { }

  ngOnInit(): void {
    this.userForm = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required]),
    });
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.userForm.controls[controlName].hasError(errorName);
  }

  public submit(userFormValue) {
    this.store.dispatch(signIn(userFormValue));
    console.log(userFormValue);
    debugger;
    // this.httpService.postData(userFormValue)
    //   .subscribe(
    //     (data: Token) => { this.token = data; this.done = true; debugger; },
    //     error => console.log(error)
    //   );
  }

}
