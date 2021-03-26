import { EStatusType } from "../enums/status-type.enum";
import { IPageOptions } from "./IPageOptions.model";

export interface IOrderPage {
    pageOptions: IPageOptions
    isDescending: boolean;
    orderByString: string;
    userId: string,
    status: EStatusType,
}