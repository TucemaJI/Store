import { IClients } from "./IClients";
import { IPageParameters } from "./IPageParameters";

export interface IPageModel {
    pageParameters: IPageParameters
    isDescending: boolean;
    orderByString: string;
    name: string;
    email: string;
}