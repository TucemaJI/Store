import { ECurrencyType } from "../enums/currency-type.enum";
import { EPrintingEditionType } from "../enums/printing-edition-type.enum";
import { IPageOptions } from "./IPageOptions.model";

export interface IPrintingEditionPage {
    pageOptions: IPageOptions
    isDescending: boolean;
    orderByString: string;
    name: string,
    title: string,
    currency: ECurrencyType,
    printingEditionTypeList: EPrintingEditionType[],
    minPrice: number,
    maxPrice: number,
}