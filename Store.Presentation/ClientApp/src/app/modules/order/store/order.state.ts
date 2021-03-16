import { IOrderModel } from "../../shared/models/IOrderModel";
import { IOrderPageModel } from "../../shared/models/IOrderPageModel";

export interface IOrderState {
    orders: IOrderModel[];
    pageModel: IOrderPageModel;
}

export const initialOrderState: IOrderState = {
    orders: null,
    pageModel: null,
}