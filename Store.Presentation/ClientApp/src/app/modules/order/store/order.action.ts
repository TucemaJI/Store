import { EStatusType } from "../../shared/enums/status-type.enum";
import { IOrderPage } from "../../shared/models/IOrderPage.model";

export enum EOrderActions {
    GetOrders = '[Order] Get Orders',
    UpdateOrder = '[Order] Update Order',
}

export class GetOrders {
    static readonly type = EOrderActions.GetOrders;
    constructor(public pageModel: IOrderPage) { }
};

export class UpdateOrder {
    static readonly type = EOrderActions.UpdateOrder;
    constructor(public orderId: number, public status: EStatusType) { }
}
