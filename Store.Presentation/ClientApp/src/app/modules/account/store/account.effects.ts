import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { EAccountActions } from "./account.actions";
import { map, catchError, exhaustMap } from 'rxjs/operators';
import { AccountHttpService } from '../../shared/services/account-http.service'
import { of } from "rxjs";
import { ILoginModel } from "../../shared/models/ILoginModel";
import { error } from "../../../store/actions/error.action"
import { Token } from "../../shared/models/Token";
import { User } from "../../shared/models/User";
import { Router } from "@angular/router";
import { IConfirmModel } from "../../shared/models/IConfirmModel";

@Injectable()
export class AccountEffects {

    login$ = createEffect(() => this.actions$.pipe(
        ofType(EAccountActions.SignIn),
        exhaustMap((action:{loginModel: ILoginModel, remember:boolean}) => this.httpService.postLogin(action.loginModel, action.remember)
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
    ));
    getUser$ = createEffect(() => this.actions$.pipe(
        ofType(EAccountActions.GetUser),
        exhaustMap((action: { userId: string }) => {
            debugger; return this.httpService.getUser(action.userId)
                .pipe(
                    map((result: User) => ({ type: EAccountActions.GetUserSuccess, user: result })),
                    catchError(err => of(error({ err })))
                )
        })
    ));
    editUser$ = createEffect(() => this.actions$.pipe(
        ofType(EAccountActions.EditUser),
        exhaustMap((action: { user: User }) => {
            debugger; return this.httpService.editUser(action.user)
                .pipe(
                    map((result: boolean) => ({ type: EAccountActions.GetUserSuccess, result: result })),
                    catchError(err => of(error({ err })))
                )
        })
    ));

    constructor(
        private actions$: Actions,
        private httpService: AccountHttpService,
        private router: Router
    ) { }
}