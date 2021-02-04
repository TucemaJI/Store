import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { EAccountActions } from "./account.actions";
import { map, mergeMap, catchError, exhaustMap } from 'rxjs/operators';
import { HttpService, Token } from '../services/HttpService'
import { of } from "rxjs";
import { ILoginModel } from "../models/ILoginModel";
import { error } from "../../../store/actions/error.action"

@Injectable()
export class AccountEffects {

    login$ = createEffect(() => this.actions$.pipe(
        ofType(EAccountActions.SignIn),
        exhaustMap((loginModel: ILoginModel) => this.httpService.postData(loginModel)
            .pipe(
                ofType(EAccountActions.SignInSuccess),
                map((responce: Token) => ({ type: EAccountActions.SignInSuccess, props: responce })),
                catchError(err => of(error({ err }))
                )

                // .subscribe(
                //   (data: Token) => { this.token = data; this.done = true; debugger; },
                //   error => console.log(error)
            )
        )
    ))
    constructor(
        private actions$: Actions,
        private httpService: HttpService,
    ) { }
}