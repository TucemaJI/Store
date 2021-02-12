import { Injectable } from "@angular/core";
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable()
export class AuthService {
    public saveToken(token: string) {
        debugger;
        localStorage.setItem('token', token);
    }
    public getToken(): string {
        debugger;
        return localStorage.getItem('token');
    }
    public isAuthenticated(): boolean {
        const token = this.getToken();
        return this.jwtHelper.isTokenExpired(token);
    }
    constructor(private jwtHelper: JwtHelperService) { }
}