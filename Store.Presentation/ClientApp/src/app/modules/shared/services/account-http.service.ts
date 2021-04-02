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

    postLogin(loginModel: ILogin, remember: boolean): Observable<IToken> {
        return this.http.post<IToken>(Consts.SIGN_IN, loginModel).pipe(
            tap(token => { this.auth.saveToken(token, remember) })
        )
    }

    postRegistration(user: IUser) {
        return this.http.post(Consts.REGISTRATION, user);
    }

    sendEmail(email: string) {
        return this.http.get(`${Consts.FORGOT_PASSWORD}${email}`);
    }

    postConfirm(model: IConfirm) {
        return this.http.post(Consts.CHECKMAIL, model);
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
        return this.http.put(Consts.UPDATE_USER, user);
    }
}
