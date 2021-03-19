import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { EAccountActions } from "./account.actions";
import { map, catchError, exhaustMap } from 'rxjs/operators';
import { AccountHttpService } from '../../shared/services/account-http.service'
import { of } from "rxjs";
import { ILogin } from "../../shared/models/ILogin.model";
import { error } from "../../../store/actions/error.action"
import { IToken } from "../../shared/models/IToken.model";
import { IUser } from "../../shared/models/IUser.model";
import { Router } from "@angular/router";
import { IConfirm } from "../../shared/models/IConfirm.model";

@Injectable()
export class AccountEffects {

    login$ = createEffect(() => this.actions$.pipe(
        ofType(EAccountActions.SignIn),
        exhaustMap((action: { loginModel: ILogin, remember: boolean }) => this.httpService.postLogin(action.loginModel, action.remember)
            .pipe(
                map((responce: IToken) => { debugger; return ({ type: EAccountActions.SignInSuccess, accessToken: responce.accessToken, refreshToken: responce.refreshToken }) }),
                catchError(err => of(error({ err })))
            )
        )
    ))

    signUp$ = createEffect(() => this.actions$.pipe(
        ofType(EAccountActions.SignUp),
        exhaustMap((user: IUser) => this.httpService.postRegistration(user)
            .pipe(
                map((result: IUser) => ({ type: EAccountActions.SignUpSuccess, user: result })),
                catchError(err => of(error({ err })))
            ))
    ))

    signUpS$ = createEffect(() => this.actions$.pipe(
        ofType(EAccountActions.SignUpSuccess),
        map(() => this.router.navigateByUrl("confirm-password"))
    ), { dispatch: false })

    confirmEmail$ = createEffect(() => this.actions$.pipe(
        ofType(EAccountActions.ConfirmEmail),
        exhaustMap((model: IConfirm) => this.httpService.postConfirm(model)
            .pipe(
                map((result: IUser) => { debugger; return ({ type: EAccountActions.ConfirmEmailSuccess, user: result }) }),
                catchError(err => of(error({ err })))
            )
        )
    ))

    confirmEmailS$ = createEffect(() => this.actions$.pipe(
        ofType(EAccountActions.ConfirmEmailSuccess),
        map(() => this.router.navigateByUrl(""))
    ), { dispatch: false })

    refreshToken$ = createEffect(() => this.actions$.pipe(
        ofType(EAccountActions.RefreshToken),
        exhaustMap((token: IToken) => this.httpService.postRefresh(token)
            .pipe(
                map((result: IToken) => { debugger; return ({ type: EAccountActions.RefreshTokenSuccess, accessToken: result.accessToken, refreshToken: result.refreshToken }) }),
                catchError(err => of(error({ err })))
            )
        )
    ));

    getUser$ = createEffect(() => this.actions$.pipe(
        ofType(EAccountActions.GetUser),
        exhaustMap((action: { userId: string }) => {
            debugger; return this.httpService.getUser(action.userId)
                .pipe(
                    map((result: IUser) => ({ type: EAccountActions.GetUserSuccess, user: result })),
                    catchError(err => of(error({ err })))
                )
        })
    ));

    editUser$ = createEffect(() => this.actions$.pipe(
        ofType(EAccountActions.EditUser),
        exhaustMap((action: { user: IUser }) => {
            debugger; return this.httpService.editUser(action.user)
                .pipe(
                    map((result: boolean) => ({ type: EAccountActions.GetUserSuccess, result: result })),
                    catchError(err => of(error({ err })))
                )
        })
    ));
    
    passwordRecovery$ = createEffect(() => this.actions$.pipe(
        ofType(EAccountActions.PasswordRecovery),
        exhaustMap((action: { email: string }) => this.httpService.sendEmail(action.email)
            .pipe(map((result: string) => ({ type: EAccountActions.PasswordRecoverySuccess, result: result })),
                catchError(err => of(error({ err })))
            )
        )
    ))

    constructor(
        private actions$: Actions,
        private httpService: AccountHttpService,
        private router: Router
    ) { }
}