import { Inject, Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { ILoginModel } from '../models/ILoginModel';

@Injectable()
export class HttpService {

    constructor(private http: HttpClient) { }

    postData(user: ILoginModel) {
        const body = { email: user.email, password: user.password };
        return this.http.post('https://localhost:44355/api/account/signin', body);
    }

    postEmail(email: Email) {
        const body = { email: email.email };
        return this.http.post('https://localhost:44355/api/account/checkmail', body);
    }
}

export class Token {
    accessToken: string;
    refreshToken: string;
}
export class Email{
    email:string;
}