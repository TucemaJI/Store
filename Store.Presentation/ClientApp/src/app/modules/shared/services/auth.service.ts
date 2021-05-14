import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { JwtHelperService } from '@auth0/angular-jwt';
import { FacebookLoginProvider, GoogleLoginProvider, SocialAuthService } from "angularx-social-login";
import { CookieService } from "ngx-cookie-service";
import { Subject } from "rxjs";
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

    public saveToken(token: IToken, remember: boolean) {
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

    public getTokens(): IToken {
        const accessToken = localStorage.getItem(Consts.ACCESS_TOKEN);
        const refToken = localStorage.getItem(Consts.REFRESH_TOKEN);
        const token: IToken = { accessToken: accessToken, refreshToken: refToken };
        return token;
    }

    public signInWithGoogle = () => {
        return this._externalAuthService.signIn(GoogleLoginProvider.PROVIDER_ID);
    }
    public signInWithFacebook = () => {
        return this._externalAuthService.signIn(FacebookLoginProvider.PROVIDER_ID);
    }
    public signOutExternal = () => {
        this._externalAuthService.signOut();
    }

    public setPhotoUrl = (photo: string): void => {
        this.cookie.set(Consts.KEY_PHOTO_URL_COOKIES, photo, Consts.EXPIRE_DAYS, '/');
    }
    public getPhoto = (): string => {
        const photoUrl = decodeURI(this.cookie.get(Consts.KEY_PHOTO_URL_COOKIES));
        return photoUrl;
    }

    constructor(private jwtHelper: JwtHelperService, private _externalAuthService: SocialAuthService, private cookie: CookieService, private http: HttpClient ) { }
}