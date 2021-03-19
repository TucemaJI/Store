import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Store } from "@ngrx/store";
import { Observable, throwError } from "rxjs";
import { catchError, retry } from "rxjs/operators";
import { IToken } from "../models/IToken.model";
import { IAppState } from "../../../store/state/app.state";
import { refreshToken } from '../../account/store/account.actions'
import { AuthService } from "../services/auth.service";
import { Consts } from "../consts";

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(public auth: AuthService, private store: Store<IAppState>) { }
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const accessToken = localStorage.getItem(Consts.ACCESS_TOKEN);
        const refToken = localStorage.getItem(Consts.REFRESH_TOKEN);
        const token: IToken = { accessToken: accessToken, refreshToken: refToken };

        return next.handle(request).pipe(
            catchError((err) => {
                if (err.status === 401) {
                    if (this.auth.isAuthenticated()) {
                        this.store.dispatch(refreshToken(token));
                    }
                    const newAccessToken = localStorage.getItem(Consts.ACCESS_TOKEN);

                    return next.handle(request.clone({
                        headers: null,
                        setHeaders: {
                            Authorization: Consts.BEARER + newAccessToken,
                        }
                    }));
                }
                return throwError(err);
            }),
        );
    }
}