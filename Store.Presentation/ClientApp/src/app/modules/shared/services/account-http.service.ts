import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { ILogin } from '../models/ILogin.model';
import { IUser } from '../models/IUser.model';
import { IConfirm } from '../models/IConfirm.model';
import { IToken } from '../models/IToken.model';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { AuthService } from './auth.service';

@Injectable()
export class AccountHttpService {

    constructor(private http: HttpClient, private auth: AuthService) { }

    postLogin(user: ILogin, remember:boolean): Observable<IToken> {
        const body = { email: user.email, password: user.password };
        debugger;
        return this.http.post<IToken>('https://localhost:44355/api/account/signin', body).pipe(
            tap(token => { this.auth.saveToken(token, remember) })
        )
    }

    postRegistration(user: IUser) {
        const body = { firstName: user.firstName, lastName: user.lastName, email: user.email, password: user.password, confirmPassword: user.confirmPassword };
        return this.http.post('https://localhost:44355/api/account/registration', body);
    }

    sendEmail(email: string) {
        debugger;
        return this.http.get(`https://localhost:44355/api/account/ForgotPassword?email=${email}`);
    }

    postConfirm(model: IConfirm) {
        const body = { email: model.email, token: model.token, password: model.password };
        return this.http.post('https://localhost:44355/api/account/checkmail', body);
    }

    postRefresh(token: IToken) {
        return this.http.post<IToken>('https://localhost:44355/api/account/refreshtoken', token).pipe(
            tap(result => { localStorage.setItem('accessToken', result.accessToken), localStorage.setItem('refreshToken', result.refreshToken) })
        );
    }

    getUser(id: string) {
        debugger;
        return this.http.get(`https://localhost:44355/api/user/getuser?id=${id}`);
    }

    editUser(user: IUser) {
        const body = { firstName: user.firstName, lastName: user.lastName, email: user.email, password: user.password, confirmPassword: user.confirmPassword, id: user.id };
        return this.http.put('https://localhost:44355/api/user/updateuser', body);
    }
}
