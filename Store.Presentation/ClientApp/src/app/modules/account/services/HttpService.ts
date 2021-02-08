import { Inject, Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { ILoginModel } from '../models/ILoginModel';
import { User } from '../models/User';

@Injectable()
export class HttpService {

    constructor(private http: HttpClient) { }

    postLogin(user: ILoginModel) {
        const body = { email: user.email, password: user.password };
        return this.http.post('https://localhost:44355/api/account/signin', body);
    }

    postRegistration(user: User) {
        const body = { firstName: user.firstName, lastName: user.lastName, email: user.email, password: user.password, confirmPassword: user.confirmPassword };
        return this.http.post('https://localhost:44355/api/account/registration', body);
    }

    postEmail(email: Email) {
        const body = { email: email.email };
        return this.http.post('https://localhost:44355/api/account/checkmail', body);
    }
}


export class Email {
    email: string;
}