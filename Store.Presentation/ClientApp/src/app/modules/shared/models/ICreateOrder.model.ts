import { IOrderItem } from "./IOrderItem.model";

export interface ICreateOrder {
    description: string,
    userId: string,
    orderItemModels: IOrderItem[],
}