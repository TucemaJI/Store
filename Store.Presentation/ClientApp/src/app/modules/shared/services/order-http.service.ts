import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Consts } from "../consts";
import { IOrder } from "../models/IOrder.model";
import { IOrderPage } from "../models/IOrderPage.model";
import { IPay } from "../models/IPay.model";

@Injectable()
export class OrderHttpService {
    constructor(private http: HttpClient) { }

    postCreateOrder(order: IOrder): Observable<Number> {
        const body = { description: order.description, userId: order.userId, orderItemModels: order.orderItemModels };
        return this.http.post<Number>(Consts.CREATE_ORDER, body);
    }
    postPay(payment: IPay) {
        const body = { cardnumber: payment.cardnumber, cvc: payment.cvc, month: payment.month, year: payment.year, orderId: payment.orderId, value: payment.value, };
        return this.http.post(Consts.PAY, body);
    }
    postGetOrders(pageModel: IOrderPage) {
        const body = {
            entityParameters: { itemsPerPage: pageModel.pageParameters.itemsPerPage, currentPage: pageModel.pageParameters.currentPage, },
            isDescending: pageModel.isDescending, orderByString: pageModel.orderByString, userId: pageModel.userId, status: pageModel.status,
        };
        return this.http.post(Consts.GET_ORDERS, body);
    }
}