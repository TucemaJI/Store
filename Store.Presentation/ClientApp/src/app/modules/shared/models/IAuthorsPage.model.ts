import { IPageOptions } from "./IPageOptions.model";

export interface IAuthorsPage {
    pageOptions: IPageOptions
    isDescending: boolean;
    orderByString: string;
    name: string;
    id: number;
}