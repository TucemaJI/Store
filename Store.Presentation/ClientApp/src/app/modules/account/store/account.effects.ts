import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { EAccountActions } from "./account.actions";
import { map, catchError, exhaustMap } from 'rxjs/operators';
import { AccountHttpService } from '../services/http.service'
import { of } from "rxjs";
import { ILoginModel } from "../models/ILoginModel";
import { error } from "../../../store/actions/error.action"
import { Token } from "../models/Token";
import { User } from "../models/User";
import { Router } from "@angular/router";
import { IConfirmModel } from "../models/IConfirmModel";

@Injectable()
export class AccountEffects {

    login$ = createEffect(() => this.actions$.pipe(
        ofType(EAccountActions.SignIn),
        exhaustMap((loginModel: ILoginModel) => this.httpService.postLogin(loginModel)
            .pipe(
                map((responce: Token) => { debugger; return ({ type: EAccountActions.SignInSuccess, accessToken: responce.accessToken, refreshToken: responce.refreshToken }) }),
                catchError(err => of(error({ err })))
            )
        )
    ))
    signUp$ = createEffect(() => this.actions$.pipe(
        ofType(EAccountActions.SignUp),
        exhaustMap((user: User) => this.httpService.postRegistration(user)
            .pipe(
                map((result: User) => ({ type: EAccountActions.SignUpSuccess, user: result })),
                catchError(err => of(error({ err })))
            ))
    ))
    signUpS$ = createEffect(() => this.actions$.pipe(
        ofType(EAccountActions.SignUpSuccess),
        map(() => this.router.navigateByUrl("confirm-password"))
    ), { dispatch: false })

    confirmPas$ = createEffect(() => this.actions$.pipe(
        ofType(EAccountActions.ConfirmPassword),
        exhaustMap((model: IConfirmModel) => this.httpService.postConfirm(model)
            .pipe(
                map((result: User) => { debugger; return ({ type: EAccountActions.ConfirmPasswordSuccess, user: result }) }),
                catchError(err => of(error({ err })))
            )
        )
    ))
    confirmPasS$ = createEffect(() => this.actions$.pipe(
        ofType(EAccountActions.ConfirmPasswordSuccess),
        map(() => this.router.navigateByUrl(""))
    ), { dispatch: false })
    refreshToken$ = createEffect(() => this.actions$.pipe(
        ofType(EAccountActions.RefreshToken),
        exhaustMap((token: Token) => this.httpService.postRefresh(token)
            .pipe(
                map((result: Token) => { debugger; return ({ type: EAccountActions.RefreshTokenSuccess, accessToken: result.accessToken, refreshToken: result.refreshToken }) }),
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