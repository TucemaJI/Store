import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Store } from "@ngrx/store";
import { Observable, of, throwError } from "rxjs";
import { catchError, exhaustMap, map, retry, tap } from "rxjs/operators";
import { Token } from "../modules/account/models/Token";
import { AccountHttpService } from "../modules/account/services/http.service";
import { IAppState } from "../store/state/app.state";
import { EAccountActions, refreshToken } from '../modules/account/store/account.actions'
import { AccountEffects } from "../modules/account/store/account.effects";
import { ofType } from "@ngrx/effects";
import { error } from "../store/actions/error.action";

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(public effects: AccountEffects, private httpService: AccountHttpService,) { }
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const accessToken = localStorage.getItem('accessToken');
        const refToken = localStorage.getItem('refreshToken');
        const token: Token = { accessToken: accessToken, refreshToken: refToken };
        
        return next.handle(request).pipe(
            catchError((err) => {
                if (err.status === 401) {
                    debugger;
                    this.effects.refreshToken$.pipe(
                        ofType(EAccountActions.RefreshToken),
                        exhaustMap((token: Token) => this.httpService.postRefresh(token)
                            .pipe(
                                map((result: Token) => { debugger; return ({ type: EAccountActions.RefreshTokenSuccess, token: result }) }),
                                catchError(err => of(error({ err })))
                            )
                        ))
                    debugger;
                    const newAccessToken = localStorage.getItem('accessToken');

                    return next.handle(request.clone({
                        setHeaders: {
                            Authorization: 'Bearer ' + newAccessToken
                        }
                    }));
                }
                return throwError(err);
            }),
        );
    }
}