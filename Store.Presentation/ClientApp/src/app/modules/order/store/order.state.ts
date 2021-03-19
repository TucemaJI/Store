import { IOrder } from "../../shared/models/IOrder.model";
import { IOrderPage } from "../../shared/models/IOrderPage.model";

export interface IOrderState {
    orders: IOrder[];
    pageModel: IOrderPage;
}

export const initialOrderState: IOrderState = {
    orders: null,
    pageModel: null,
}