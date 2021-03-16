import { createAction, props } from "@ngrx/store";
import { IOrderModel } from "../../shared/models/IOrderModel";
import { IOrderPageModel } from "../../shared/models/IOrderPageModel";
import { IPageParameters } from "../../shared/models/IPageParameters";

export enum EOrderActions {
    GetOrders = '[Order] Get Orders',
    GetOrdersSuccess = '[Order] Get Orders Success',

}

export const getOrders = createAction(EOrderActions.GetOrders, props<{ pageModel: IOrderPageModel }>());
export const getOrderSuccess = createAction(EOrderActions.GetOrdersSuccess, props<{ pageParameters: IPageParameters, orders: IOrderModel[] }>());