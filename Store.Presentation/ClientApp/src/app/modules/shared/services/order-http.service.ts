import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Consts } from "../consts";
import { ICreateOrder } from "../models/ICreateOrder.model";
import { IOrder } from "../models/IOrder.model";
import { IOrderPage } from "../models/IOrderPage.model";
import { IPay } from "../models/IPay.model";

@Injectable()
export class OrderHttpService {
    constructor(private http: HttpClient) { }

    postCreateOrder(order: ICreateOrder): Observable<Number> {
        return this.http.post<Number>(Consts.CREATE_ORDER, order);
    }
    postPay(payment: IPay) {
        return this.http.post(Consts.PAY, payment);
    }
    postGetOrders(pageModel: IOrderPage) {
        return this.http.post(Consts.GET_ORDERS, pageModel);
    }
}