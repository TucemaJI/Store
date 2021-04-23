import { ICreateOrder } from "../../shared/models/ICreateOrder.model";
import { IPay } from "../../shared/models/IPay.model";

export enum ECartActions {
    CreateOrder = '[Cart] Create Order',
    Pay = '[Cart] Pay',
}

export class CreateOrder {
    static readonly type = ECartActions.CreateOrder;
    constructor(public order: ICreateOrder) { }
};
export class Pay {
    static readonly type = ECartActions.Pay;
    constructor(public payment: IPay, public orderId: number) { }
};


