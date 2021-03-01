import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { tap } from "rxjs/operators";
import { IPrintingEdition } from "../models/IPrintingEdition";
import { IPrintingEditionPageModel } from "../models/IPrintingEditionPageModel";

@Injectable()
export class PrintingEditionHttpService {

    constructor(private http: HttpClient) { }

    postPE(pageModel: IPrintingEditionPageModel) {
        const body = {
            entityParameters: { itemsPerPage: pageModel.pageParameters.itemsPerPage, currentPage: pageModel.pageParameters.currentPage },
            isDescending: pageModel.isDescending, orderByString: pageModel.orderByString,
            name: pageModel.name, title: pageModel.title, currency: pageModel.currency, pEType: pageModel.pEType, minPrice: pageModel.minPrice, maxPrice: pageModel.maxPrice,
        };
        debugger;
        return this.http.post('https://localhost:44355/api/printingedition/getprintingeditions', body);
    }

}
