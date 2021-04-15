import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Store } from "@ngxs/store";
import { Observable, throwError } from "rxjs";
import { catchError } from "rxjs/operators";
import { IToken } from "../models/IToken.model";
import { RefreshToken } from '../../account/store/account.actions'
import { AuthService } from "../services/auth.service";
import { Consts } from "../consts";

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(public auth: AuthService, private store: Store) { }
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        const token: IToken = this.auth.getTokens();

        return next.handle(request).pipe(
            catchError((err) => {
                if (err.status === 401) {
                    if (!this.auth.isAuthenticated()) {
                        this.store.dispatch(new RefreshToken( token ));
                    }
                    const newAccessToken = localStorage.getItem(Consts.ACCESS_TOKEN);

                    return next.handle(request.clone({
                        headers: null,
                        setHeaders: {
                            Authorization: `${Consts.BEARER} ${newAccessToken}`,
                        }
                    }));
                }
                if (err.status === 400) {
                    alert(err.error.Errors);
                }
                if (err.status === 403) {
                    alert(Consts.FORBIDDEN);
                }
                return throwError(err);
            }),
        );
    }
}