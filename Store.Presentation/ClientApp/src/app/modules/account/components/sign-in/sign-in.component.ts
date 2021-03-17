import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { IAppState } from 'src/app/store/state/app.state';
import { ILoginModel } from '../../../shared/models/ILoginModel';
import { AccountHttpService } from '../../../shared/services/account-http.service';
import { EAccountActions, signIn } from '../../store/account.actions';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit {
  userForm: FormGroup;
  remember: boolean = false;

  constructor(private store: Store<IAppState>, private router: Router) { }

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
    const model: ILoginModel = {
      email: userFormValue.email,
      password: userFormValue.password,
    }
    this.store.dispatch(signIn({ loginModel: model, remember: this.remember }));
    this.router.navigateByUrl('');
  }

}
