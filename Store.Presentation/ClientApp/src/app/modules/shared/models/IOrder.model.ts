import { EStatusType } from "../enums/status-type.enum";
import { IOrderItem } from "./IOrderItem.model";

export interface IOrder {
    description: string;
    userId: string;
    paymentId: number;
    status: EStatusType;
    orderItemModels: IOrderItem[];
    isRemoved: boolean;
    totalAmount: number;
    id: number;
    userName: string;
    userEmail: string;
}