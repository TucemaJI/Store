import { IPageParameters } from "./IPageParameters.model";

export interface IAuthorsPage {
    pageParameters: IPageParameters
    isDescending: boolean;
    orderByString: string;
    name: string;
    id: number;
}