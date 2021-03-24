import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { ILogin } from '../models/ILogin.model';
import { IUser } from '../models/IUser.model';
import { IConfirm } from '../models/IConfirm.model';
import { IToken } from '../models/IToken.model';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { AuthService } from './auth.service';
import { Consts } from '../consts';

@Injectable()
export class AccountHttpService {

    constructor(private http: HttpClient, private auth: AuthService) { }

    postLogin(user: ILogin, remember: boolean): Observable<IToken> {
        const body = { email: user.email, password: user.password };
        return this.http.post<IToken>(Consts.SIGN_IN, body).pipe(
            tap(token => { this.auth.saveToken(token, remember) })
        )
    }

    postRegistration(user: IUser) {
        const body = { firstName: user.firstName, lastName: user.lastName, email: user.email, password: user.password, confirmPassword: user.confirmPassword };
        return this.http.post(Consts.REGISTRATION, body);
    }

    sendEmail(email: string) {
        return this.http.get(`${Consts.FORGOT_PASSWORD}${email}`);
    }

    postConfirm(model: IConfirm) {
        const body = { email: model.email, token: model.token};
        return this.http.post(Consts.CHECKMAIL, body);
    }

    postRefresh(token: IToken) {
        return this.http.post<IToken>(Consts.POST_REFRESH_TOKEN, token).pipe(
            tap(result => { localStorage.setItem(Consts.ACCESS_TOKEN, result.accessToken), localStorage.setItem(Consts.REFRESH_TOKEN, result.refreshToken) })
        );
    }

    getUser(id: string) {
        return this.http.get(`${Consts.GET_USER}${id}`);
    }

    editUser(user: IUser) {
        const body = { firstName: user.firstName, lastName: user.lastName, email: user.email, password: user.password, confirmPassword: user.confirmPassword, id: user.id };
        return this.http.put(Consts.UPDATE_USER, body);
    }
}
