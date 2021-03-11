import { createAction, props } from "@ngrx/store";
import { IOrderModel } from "../../shared/models/IOrderModel";
import { IPayModel } from "../../shared/models/IPayModel";

export enum ECartActions {
    CreateOrder = '[Cart] Create Order',
    CreateOrderSuccess = '[Cart] Create Order Success',
    Pay = '[Cart] Pay',
    PaySuccess = '[Cart] Pay Success',

}

export const createOrder = createAction(ECartActions.CreateOrder, props<{ order: IOrderModel }>());
export const createOrderSuccess = createAction(ECartActions.CreateOrderSuccess, props<{ orderId: number }>());
export const pay = createAction(ECartActions.Pay, props<{ payment: IPayModel }>());
export const paySuccess = createAction(ECartActions.PaySuccess, props<{ result: boolean }>());


