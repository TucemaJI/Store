import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable()
export class AccountHttpService {

    constructor(private http: HttpClient) { }

    // getPE(): Observable<IPrintingEdition> {
    //     const body = { email: user.email, password: user.password };
    //     debugger;
    //     return this.http.post<Token>('https://localhost:44355/api/account/signin', body).pipe(
    //         tap(token => { this.auth.saveToken(token) })
    //     )
    // }

}
