import { ECurrencyType } from "./currency-type.enum";
import { EPrintingEditionType } from "./printing-edition-type.enum";
import { IPageParameters } from "./IPageParameters.model";

export interface IPrintingEditionPage {
    pageParameters: IPageParameters
    isDescending: boolean;
    orderByString: string;
    name: string,
    title: string,
    currency: ECurrencyType,
    printingEditionTypeList: EPrintingEditionType[],
    minPrice: number,
    maxPrice: number,
}