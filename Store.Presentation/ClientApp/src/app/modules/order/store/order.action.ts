import { createAction, props } from "@ngrx/store";
import { IOrder } from "../../shared/models/IOrder.model";
import { IOrderPage } from "../../shared/models/IOrderPage.model";
import { IPageParameters } from "../../shared/models/IPageParameters.model";

export enum EOrderActions {
    GetOrders = '[Order] Get Orders',
    GetOrdersSuccess = '[Order] Get Orders Success',

}

export const getOrders = createAction(EOrderActions.GetOrders, props<{ pageModel: IOrderPage }>());
export const getOrderSuccess = createAction(EOrderActions.GetOrdersSuccess, props<{ pageParameters: IPageParameters, orders: IOrder[] }>());