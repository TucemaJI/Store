import { Inject, Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { ILoginModel } from '../models/ILoginModel';
import { User } from '../models/User';
import { IConfirmModel } from '../models/IConfirmModel';
import { Token } from '../models/Token';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { AuthService } from './auth.service';
import { error } from 'src/app/store/actions/error.action';

@Injectable()
export class AccountHttpService {

    constructor(private http: HttpClient, private auth: AuthService) { }

    postLogin(user: ILoginModel): Observable<Token> {
        const body = { email: user.email, password: user.password };
        debugger;
        return this.http.post<Token>('https://localhost:44355/api/account/signin', body).pipe(
            tap(token => { this.auth.saveToken(token) })
        )
    }

    postRegistration(user: User) {
        const body = { firstName: user.firstName, lastName: user.lastName, email: user.email, password: user.password, confirmPassword: user.confirmPassword };
        return this.http.post('https://localhost:44355/api/account/registration', body);
    }

    postEmail(email: Email) {
        const body = { email: email.email };
        debugger;
        return this.http.post('https://localhost:44355/api/account/ForgotPassword', body);
    }

    postConfirm(model: IConfirmModel) {
        const body = { email: model.email, token: model.token, password: model.password };
        return this.http.post('https://localhost:44355/api/account/checkmail', body);
    }

    postRefresh(oldToken: Token): Token {
        const body = { AccessToken: oldToken.accessToken, RefreshToken: oldToken.refreshToken };
        debugger;
        return this.http.post<Token>('https://localhost:44355/api/account/refreshtoken', body).subscribe(
            (accessToken, refreshToken) => { this.auth.saveToken(token:{accessToken, refreshToken}), catchError(err => of(error({ err }))) })
            
        
    }
}


export class Email {
    email: string;
}