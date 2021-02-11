import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { AuthService } from "../modules/account/services/auth.service";

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  constructor(public auth: AuthService) { }
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    debugger;
    const tok = localStorage.getItem('token');
    
    request = request.clone({
      setHeaders: {
        Authorization: 'Bearer ' + tok
      }
    });
    return next.handle(request);
  }
}