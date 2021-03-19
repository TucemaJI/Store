import { IOrderItem } from "./IOrderItem.model";

export interface IOrder {
    description: string;
    userId: string;
    paymentId: number;
    status: boolean;
    orderItemModels: IOrderItem[];
    isRemoved: boolean;
    totalAmount: number;
    id: number;
}