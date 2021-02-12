import { Injectable } from "@angular/core";
import { JwtHelperService } from '@auth0/angular-jwt';
import { Token } from "../models/Token";

@Injectable()
export class AuthService {
    public saveToken(token: Token) {
        debugger;
        console.log("NEW TOKEN");
        localStorage.setItem('accessToken', token.accessToken);
        localStorage.setItem('refreshToken', token.refreshToken);
    }
    public getToken(): string {
        debugger;
        return localStorage.getItem('accessToken');
    }
    public isAuthenticated(): boolean {
        const token = this.getToken();
        return this.jwtHelper.isTokenExpired(token);
    }
   
    constructor(private jwtHelper: JwtHelperService) { }
}