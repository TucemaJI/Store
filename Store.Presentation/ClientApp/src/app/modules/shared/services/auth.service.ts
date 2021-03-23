import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { JwtHelperService } from '@auth0/angular-jwt';
import { Subject } from "rxjs";
import { tap } from "rxjs/operators";
import { Consts } from "../consts";
import { IToken } from "../models/IToken.model";

@Injectable()
export class AuthService {

    userIdChanged = new Subject<string>();

    public isAdmin(): boolean {
        const token = this.getToken();
        const decoded = this.jwtHelper.decodeToken(token);
        const role = decoded[Consts.ROLE];
        if (role === Consts.ADMIN) {
            return true;
        }
        return false;
    }

    public getId(): string {
        const token = this.getToken();
        if (token !== null) {
            const id = this.jwtHelper.decodeToken(token)[Consts.IDENTIFIER];
            return id;
        }
    }

    public saveToken(token, remember: boolean) {
        localStorage.setItem(Consts.ACCESS_TOKEN, token.accessToken);
        if (remember) {
            localStorage.setItem(Consts.REFRESH_TOKEN, token.refreshToken);
        }
        const id = this.jwtHelper.decodeToken(token.accessToken)[Consts.IDENTIFIER];
        this.userIdChanged.next(id);
    }

    public getToken(): string {
        return localStorage.getItem(Consts.ACCESS_TOKEN);
    }
    
    public isAuthenticated(): boolean {
        const token = this.getToken();
        const isAuth = !this.jwtHelper.isTokenExpired(token);
        if (isAuth) {
            const id = this.jwtHelper.decodeToken(token)[Consts.IDENTIFIER];
            this.userIdChanged.next(id);
        }
        return isAuth;
    }

    constructor(private http: HttpClient, private jwtHelper: JwtHelperService) { }
}