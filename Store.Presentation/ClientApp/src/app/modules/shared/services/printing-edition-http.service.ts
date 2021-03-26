import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Consts } from "../consts";
import { IPrintingEditionPage } from "../models/IPrintingEditionPage.model";

@Injectable()
export class PrintingEditionHttpService {

    constructor(private http: HttpClient) { }

    postPE(pageModel: IPrintingEditionPage) {
        return this.http.post(Consts.GET_PES, pageModel);
    }
    getPE(id: number) {
        return this.http.get(`${Consts.GET_PE}${id}`);
    }

}
