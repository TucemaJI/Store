import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { IOrderModel } from "../models/IOrderModel";
import { IPayModel } from "../models/IPayModel";

@Injectable()
export class OrderHttpService {
    constructor(private http: HttpClient) { }

    postCreateOrder(order: IOrderModel): Observable<Number> {
        const body = { description: order.description, userId: order.userId, orderItemModels: order.orderItemModels };
        debugger;
        return this.http.post<Number>('https://localhost:44355/api/order/createorder', body)
    }
    postPay(payment: IPayModel){
        const body = { cardnumber: payment.cardnumber, cvc: payment.cvc, month: payment.month, year: payment.year, orderId: payment.orderId, value: payment.value, };
        debugger;
        return this.http.post('https://localhost:44355/api/order/pay', body)
    }
}