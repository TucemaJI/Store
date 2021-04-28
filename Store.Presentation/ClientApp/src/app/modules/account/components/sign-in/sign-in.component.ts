import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Store } from '@ngxs/store';
import { ILogin } from '../../../shared/models/ILogin.model';
import { SignIn, SignInByFacebook, SignInByGoogle as SignInByGoogle } from '../../store/account.actions';
import { DOCUMENT } from '@angular/common';
import { AuthService } from 'src/app/modules/shared/services/auth.service';
import { SocialUser } from 'angularx-social-login';
import { IExternalAuth } from 'src/app/modules/shared/models/IExternalAuth.model';
import { IFacebookUserModel } from 'src/app/modules/shared/models/IFacebookUser.model';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit {
  public userForm: FormGroup;
  public remember: boolean = false;
  public loginForm: FormGroup;

  constructor(@Inject(DOCUMENT) private document: Document, private store: Store, private authService: AuthService) { }

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

  public externalLogin = () => {
    this.authService.signInWithGoogle()
      .then(res => {
        const user: SocialUser = { ...res };
        this.authService.setPhotoUrl(user.photoUrl);
        const externalAuth: IExternalAuth = {
          provider: user.provider,
          idToken: user.idToken
        }
        this.validateExternalAuth(externalAuth);
      }, error => console.log(error))
  }

  public loginWithFacebook(): void {
    this.authService.signInWithFacebook().then(
      res=>{
        this.authService.setPhotoUrl(res.photoUrl);
        const user: IFacebookUserModel = { email: res.email, firstName: res.firstName, lastName: res.lastName };
        this.store.dispatch(new SignInByFacebook(user, this.remember))
        
      }, error => console.log(error))
  }

  private validateExternalAuth(externalAuth: IExternalAuth) {
    this.store.dispatch(new SignInByGoogle(externalAuth, this.remember));
  }
}
