import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Consts } from "../consts";
import { ICreateOrder } from "../models/ICreateOrder.model";
import { IOrderPage } from "../models/IOrderPage.model";
import { IPay } from "../models/IPay.model";
import { IResultPageModel } from "../models/IResultPage.model";

@Injectable()
export class OrderHttpService {
    constructor(private http: HttpClient) { }

    postCreateOrder(order: ICreateOrder): Observable<number> {
        return this.http.post<number>(Consts.CREATE_ORDER, order);
    }
    postPay(payment: IPay):Observable<boolean> {
        return this.http.post<boolean>(Consts.PAY, payment);
    }
    postGetOrders(pageModel: IOrderPage):Observable<IResultPageModel> {
        return this.http.post<IResultPageModel>(Consts.GET_ORDERS, pageModel);
    }
    fakeMethod():Observable<IResultPageModel>{
        return new Observable<IResultPageModel>(
            
        )
    }
}