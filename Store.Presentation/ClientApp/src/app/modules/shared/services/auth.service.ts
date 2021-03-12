import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { JwtHelperService } from '@auth0/angular-jwt';
import { resultMemoize } from "@ngrx/store";
import { Subject } from "rxjs";
import { tap } from "rxjs/operators";
import { Token } from "../models/Token";

@Injectable()
export class AuthService {

    userIdChanged = new Subject<string>();

    public isAdmin(): boolean {
        const token = this.getToken();
        const decoded = this.jwtHelper.decodeToken(token);
        const role = decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
        if (role === "Admin") {
            return true;
        }
        return false;
    }

    public getId(): string {
        const token = this.getToken();
        if (token !== null) {
            const id = this.jwtHelper.decodeToken(token)["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
            return id;
        }
    }

    public saveToken(token) {
        debugger;
        console.log("NEW TOKEN");
        localStorage.setItem('accessToken', token.accessToken);
        localStorage.setItem('refreshToken', token.refreshToken);
        const id = this.jwtHelper.decodeToken(token.accessToken)["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
        this.userIdChanged.next(id);
    }
    public getToken(): string {
        debugger;
        return localStorage.getItem('accessToken');
    }
    public isAuthenticated(): boolean {
        const token = this.getToken();
        const isAuth = !this.jwtHelper.isTokenExpired(token);
        if (isAuth) {
            const id = this.jwtHelper.decodeToken(token)["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
            this.userIdChanged.next(id);
        }
        debugger;
        return isAuth;
    }
    public refreshToken(token) {
        return this.http.post<Token>('https://localhost:44355/api/account/refreshtoken', token).pipe(
            tap(result => { localStorage.setItem('accessToken', result.accessToken), localStorage.setItem('refreshToken', result.refreshToken) })
        )
    }

    constructor(private http: HttpClient, private jwtHelper: JwtHelperService) { }
}