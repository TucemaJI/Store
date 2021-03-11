import { IOrderItemModel } from "./IOrderItemModel";

export interface IOrderModel{
    description: string;
    userId: string;
    paymentId: number;
    status: boolean;
    orderItems: IOrderItemModel[];
}