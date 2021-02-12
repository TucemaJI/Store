import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, throwError } from "rxjs";
import { catchError, retry, tap } from "rxjs/operators";
import { Token } from "../modules/account/models/Token";
import { AccountHttpService } from "../modules/account/services/http.service";

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(public http: AccountHttpService) { }
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const tok = localStorage.getItem('accessToken');
        const rTok = localStorage.getItem('refreshToken');
        const token = { accessToken: tok, refreshToken: rTok };
        let res;
        return next.handle(request).pipe(

            catchError((err) => {
                if (err.status === 401) {

                    const test = this.http.postRefresh(token);

                    const newTok = localStorage.getItem('accessToken');
                    debugger;
                    return next.handle(request.clone({
                        setHeaders: {
                            Authorization: 'Bearer ' + newTok
                        }
                    }));
                }
                return throwError(err);
            }),
            retry(1),
        );
    }
}