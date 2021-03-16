import { EStatus } from "./EStatus";
import { IPageParameters } from "./IPageParameters";

export interface IOrderPageModel {
    pageParameters: IPageParameters
    isDescending: boolean;
    orderByString: string;
    userId: string,
    status: EStatus,
}