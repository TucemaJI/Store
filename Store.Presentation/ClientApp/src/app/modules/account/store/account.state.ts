import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { Action, Selector, State, StateContext } from "@ngxs/store";
import { tap } from "rxjs/operators";
import { Consts } from "../../shared/consts";
import { IConfirm } from "../../shared/models/IConfirm.model";
import { IExternalAuth } from "../../shared/models/IExternalAuth.model";
import { IFacebookUserModel } from "../../shared/models/IFacebookUser.model";
import { ILogin } from "../../shared/models/ILogin.model";
import { IToken } from "../../shared/models/IToken.model";
import { IUser } from "../../shared/models/IUser.model";
import { AccountHttpService } from "../../shared/services/account-http.service";
import { ConfirmEmail, EditUser, GetUser, PasswordRecovery, RefreshToken, SignIn, SignInByFacebook, SignInByGoogle, SignUp } from "./account.actions";

export interface IAccountState {
  user: IUser
  token: IToken
}

@State<IAccountState>({
  name: 'account',
  defaults: {
    user: null,
    token: null,
  }
})

@Injectable()
export class AccountState {
  constructor(private accountService: AccountHttpService, private router: Router) { }

  @Selector()
  static getUserState(state: IAccountState) {
    return state.user;
  }

  @Selector()
  static getTokenState(state: IAccountState) {
    return state.token;
  }

  @Action(SignIn)
  login({ getState, setState }: StateContext<IAccountState>, payload: { loginModel: ILogin, remember: boolean }) {
    return this.accountService.postLogin(payload.loginModel, payload.remember).pipe(
      tap(result => {
        const state = getState();
        setState({
          ...state,
          token: result
        });
        this.router.navigateByUrl("");
      })
    );
  }

  @Action(SignInByGoogle)
  loginByGoogle({ getState, setState }: StateContext<IAccountState>, payload: { externalAuth: IExternalAuth, remember: boolean }) {
    return this.accountService.postLoginByGoogle(payload.externalAuth, payload.remember).pipe(
      tap(result => {
        const state = getState();
        setState({
          ...state,
          token: result
        });
        this.router.navigateByUrl("");
      })
    );
  }

  @Action(SignInByFacebook)
  loginByFacebook({ getState, setState }: StateContext<IAccountState>, payload: { user: IFacebookUserModel, remember: boolean }) {
    return this.accountService.postLoginByFacebook(payload.user, payload.remember).pipe(
      tap(result => {
        const state = getState();
        setState({
          ...state,
          token: result
        });
        this.router.navigateByUrl("");
      })
    );
  }

  @Action(SignUp)
  signUp(payload: { user: IUser }) {
    return this.accountService.postRegistration(payload.user).pipe(
      tap(() => {
        this.router.navigateByUrl(Consts.ROUTE_CONFIRM_EMAIL);
      })
    );
  }

  @Action(ConfirmEmail)
  confirmEmail(payload: { model: IConfirm }) {
    return this.accountService.postConfirm(payload.model).pipe(
      tap(() => {
        this.router.navigateByUrl(Consts.ROUTE_SIGN_IN);
      })
    );
  }

  @Action(RefreshToken)
  refreshToken({ getState, setState }: StateContext<IAccountState>, payload: { token: IToken }) {
    return this.accountService.postRefresh(payload.token).pipe(
      tap(result => {
        const state = getState();
        setState({
          ...state,
          token: result
        });
      })
    );
  }

  @Action(GetUser)
  getUser({ getState, setState }: StateContext<IAccountState>, payload: { userId: string }) {
    return this.accountService.getUser(payload.userId).pipe(
      tap(result => {
        const state = getState();
        setState({
          ...state,
          user: result
        });
      })
    );
  }

  @Action(EditUser)
  editUser({ getState, setState }: StateContext<IAccountState>, payload: { user: IUser }) {
    return this.accountService.editUser(payload.user).pipe(
      tap(result => {
        const state = getState();
        setState({
          ...state,
          user: result
        });
      })
    );
  }

  @Action(PasswordRecovery)
  passwordRecovery(payload: { email: string }) {
    return this.accountService.sendEmail(payload.email);
  }
}
