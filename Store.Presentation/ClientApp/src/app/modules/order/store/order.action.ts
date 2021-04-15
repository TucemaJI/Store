import { IOrderPage } from "../../shared/models/IOrderPage.model";

export enum EOrderActions {
    GetOrders = '[Order] Get Orders',
}

export class GetOrders {
    static readonly type = EOrderActions.GetOrders;
    constructor(public pageModel: IOrderPage) { }
};