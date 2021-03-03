import { ECurrencyType } from "./ECurrencyType";
import { EPrintingEditionType } from "./EPrintingEditionType";
import { IPageParameters } from "./IPageParameters";

export interface IPrintingEditionPageModel {
    pageParameters: IPageParameters
    isDescending: boolean;
    orderByString: string;
    name: string,
    title: string,
    currency: ECurrencyType,
    pEType: EPrintingEditionType[],
    minPrice: number,
    maxPrice: number,
}