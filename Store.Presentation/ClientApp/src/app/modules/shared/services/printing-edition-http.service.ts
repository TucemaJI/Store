import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Consts } from "../consts";
import { IPrintingEdition } from "../models/IPrintingEdition.model";
import { IPrintingEditionPage } from "../models/IPrintingEditionPage.model";
import { IResultPageModel } from "../models/IResultPage.model";
import { IResultPrintingEditionPageModel } from "../models/IResultPrintingEditionPage.model";

@Injectable()
export class PrintingEditionHttpService {

    constructor(private http: HttpClient) { }

    postPE(pageModel: IPrintingEditionPage): Observable<IResultPrintingEditionPageModel> {
        return this.http.post<IResultPrintingEditionPageModel>(Consts.GET_PES, pageModel);
    }
    getPE(id: number): Observable<IPrintingEdition> {
        return this.http.get<IPrintingEdition>(`${Consts.GET_PE}${id}`);
    }

}
