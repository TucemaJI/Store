import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { JwtHelperService } from '@auth0/angular-jwt';
import { tap } from "rxjs/operators";
import { Token } from "../models/Token";

@Injectable()
export class AuthService {

    public isAdmin(): boolean{
        const token = this.getToken();
        const decoded = this.jwtHelper.decodeToken(token);
        const role = decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
        if(role === "Admin"){
            return true;
        }
        return false;
    }

    public saveToken(token) {
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
    public refreshToken(token) {
        this.http.post<Token>('https://localhost:44355/api/account/refreshtoken', token).subscribe(
            result => { localStorage.setItem('accessToken', result.accessToken), localStorage.setItem('refreshToken', result.refreshToken) }
        )
    }

    constructor(private http: HttpClient, private jwtHelper: JwtHelperService) { }
}