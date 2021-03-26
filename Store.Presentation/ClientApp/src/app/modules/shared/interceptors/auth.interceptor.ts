import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Consts } from "../consts";
import { AuthService } from "../services/auth.service";

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  constructor(public auth: AuthService) { }
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = localStorage.getItem(Consts.ACCESS_TOKEN);

    request = request.clone({
      setHeaders: {
        Authorization: `${Consts.BEARER} ${token}`,
      }
    });
    return next.handle(request);
  }
}