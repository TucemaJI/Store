import { EStatusType } from "./status-type.enum";
import { IPageParameters } from "./IPageParameters.model";

export interface IOrderPage {
    pageParameters: IPageParameters
    isDescending: boolean;
    orderByString: string;
    userId: string,
    status: EStatusType,
}