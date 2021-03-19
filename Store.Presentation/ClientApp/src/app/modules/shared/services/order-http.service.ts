import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { IOrder } from "../models/IOrder.model";
import { IOrderPage } from "../models/IOrderPage.model";
import { IPay } from "../models/IPay.model";

@Injectable()
export class OrderHttpService {
    constructor(private http: HttpClient) { }

    postCreateOrder(order: IOrder): Observable<Number> {
        const body = { description: order.description, userId: order.userId, orderItemModels: order.orderItemModels };
        debugger;
        return this.http.post<Number>('https://localhost:44355/api/order/createorder', body)
    }
    postPay(payment: IPay) {
        const body = { cardnumber: payment.cardnumber, cvc: payment.cvc, month: payment.month, year: payment.year, orderId: payment.orderId, value: payment.value, };
        debugger;
        return this.http.post('https://localhost:44355/api/order/pay', body)
    }
    postGetOrders(pageModel: IOrderPage) {
        const body = {
            entityParameters: { itemsPerPage: pageModel.pageParameters.itemsPerPage, currentPage: pageModel.pageParameters.currentPage },
            isDescending: pageModel.isDescending, orderByString: pageModel.orderByString, status: pageModel.status, userId: pageModel.userId,
        };
        debugger;
        return this.http.post('https://localhost:44355/api/order/GetOrders', body);
    }
}