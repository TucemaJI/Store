import { IPageParameters } from "./IPageParameters";

export interface IClientsPageModel {
    pageParameters: IPageParameters
    isDescending: boolean;
    orderByString: string;
    name: string;
    email: string;
    isBlocked: boolean;
}