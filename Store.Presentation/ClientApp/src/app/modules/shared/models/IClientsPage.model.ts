import { IPageOptions } from "./IPageOptions.model";

export interface IClientsPage {
    pageOptions: IPageOptions
    isDescending: boolean;
    orderByString: string;
    name: string;
    email: string;
    isBlocked: boolean;
}