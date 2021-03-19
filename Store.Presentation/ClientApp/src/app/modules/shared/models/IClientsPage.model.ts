import { IPageParameters } from "./IPageParameters.model";

export interface IClientsPage {
    pageParameters: IPageParameters
    isDescending: boolean;
    orderByString: string;
    name: string;
    email: string;
    isBlocked: boolean;
}