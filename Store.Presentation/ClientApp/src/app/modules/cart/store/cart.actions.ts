import { createAction, props } from "@ngrx/store";
import { IOrder } from "../../shared/models/IOrder.model";
import { IPay } from "../../shared/models/IPay.model";

export enum ECartActions {
    CreateOrder = '[Cart] Create Order',
    CreateOrderSuccess = '[Cart] Create Order Success',
    Pay = '[Cart] Pay',
    PaySuccess = '[Cart] Pay Success',

}

export const createOrder = createAction(ECartActions.CreateOrder, props<{ order: IOrder }>());
export const createOrderSuccess = createAction(ECartActions.CreateOrderSuccess, props<{ orderId: number }>());
export const pay = createAction(ECartActions.Pay, props<{ payment: IPay }>());
export const paySuccess = createAction(ECartActions.PaySuccess, props<{ result: boolean }>());


