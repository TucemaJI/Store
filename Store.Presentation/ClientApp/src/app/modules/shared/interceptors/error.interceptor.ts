import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Store } from "@ngrx/store";
import { Observable, of, throwError } from "rxjs";
import { catchError, exhaustMap, map, retry, tap } from "rxjs/operators";
import { Token } from "../models/Token";
import { AccountHttpService } from "../services/account-http.service";
import { IAppState } from "../../../store/state/app.state";
import { EAccountActions, refreshToken } from '../../account/store/account.actions'
import { AccountEffects } from "../../account/store/account.effects";
import { ofType } from "@ngrx/effects";
import { error } from "../../../store/actions/error.action";
import { AuthService } from "../services/auth.service";

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(public auth: AuthService, private store: Store<IAppState>) { }
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const accessToken = localStorage.getItem('accessToken');
        const refToken = localStorage.getItem('refreshToken');
        const token: Token = { accessToken: accessToken, refreshToken: refToken };

        return next.handle(request).pipe(
            catchError((err) => {
                if (err.status === 401) {
                    if (this.auth.isAuthenticated()) {
                        this.store.dispatch(refreshToken(token));
                    }
                    const newAccessToken = localStorage.getItem('accessToken');

                    return next.handle(request.clone({
                        headers: null,
                        setHeaders: {
                            Authorization: 'Bearer ' + newAccessToken
                        }
                    }));
                }
                return throwError(err);
            }), retry(1),
        );
    }
}