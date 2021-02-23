import { IPageParameters } from "./IPageParameters";

export interface IAuthorsPageModel {
    pageParameters: IPageParameters
    isDescending: boolean;
    orderByString: string;
    name: string;
    id: number;
}