import { Router } from "@angular/router";
import { Action, Selector, State, StateContext } from "@ngxs/store";
import { tap } from "rxjs/operators";
import { Consts } from "../../shared/consts";
import { IConfirm } from "../../shared/models/IConfirm.model";
import { ILogin } from "../../shared/models/ILogin.model";
import { IToken } from "../../shared/models/IToken.model";
import { IUser } from "../../shared/models/IUser.model";
import { AccountHttpService } from "../../shared/services/account-http.service";
import { ConfirmEmail, EditUser, GetUser, PasswordRecovery, RefreshToken, SignIn, SignUp } from "./account.actions";

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
  login({ getState, setState }: StateContext<IAccountState>, loginModel: ILogin, remember: boolean) {
    return this.accountService.postLogin(loginModel, remember).pipe(
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
  signUp(user: IUser) {
    return this.accountService.postRegistration(user).pipe(
      tap(() => {
        this.router.navigateByUrl(Consts.ROUTE_CONFIRM_EMAIL);
      })
    );
  }

  @Action(ConfirmEmail)
  confirmEmail(model: IConfirm) {
    return this.accountService.postConfirm(model).pipe(
      tap(() => {
        this.router.navigateByUrl(Consts.ROUTE_SIGN_IN);
      })
    );
  }

  @Action(RefreshToken)
  refreshToken({ getState, setState }: StateContext<IAccountState>, token: IToken) {
    return this.accountService.postRefresh(token).pipe(
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
  getUser({ getState, setState }: StateContext<IAccountState>, userId: string) {
    return this.accountService.getUser(userId).pipe(
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
  editUser({ getState, setState }: StateContext<IAccountState>, user: IUser) {
    return this.accountService.editUser(user).pipe(
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
  passwordRecovery(email: string) {
    return this.accountService.sendEmail(email);
  }
}
