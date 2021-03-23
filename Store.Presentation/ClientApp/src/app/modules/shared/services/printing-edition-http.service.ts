import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Consts } from "../consts";
import { IPrintingEditionPage } from "../models/IPEPage.model";

@Injectable()
export class PrintingEditionHttpService {

    constructor(private http: HttpClient) { }

    postPE(pageModel: IPrintingEditionPage) {
        const body = {
            entityParameters: { itemsPerPage: pageModel.pageParameters.itemsPerPage, currentPage: pageModel.pageParameters.currentPage },
            isDescending: pageModel.isDescending, orderByString: pageModel.orderByString,
            name: pageModel.name, title: pageModel.title, currency: pageModel.currency, printingEditionTypeList: pageModel.printingEditionTypeList,
            minPrice: pageModel.minPrice, maxPrice: pageModel.maxPrice,
        };
        return this.http.post(Consts.GET_PES, body);
    }
    getPE(id: number) {
        return this.http.get(`${Consts.GET_PE}${id}`);
    }

}
