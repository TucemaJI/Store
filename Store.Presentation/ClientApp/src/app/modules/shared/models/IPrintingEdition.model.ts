import { ECurrencyType } from "../enums/currency-type.enum";
import { EPrintingEditionType } from "../enums/printing-edition-type.enum";

export interface IPrintingEdition {
    id: number;
    title: string;
    currencyType: ECurrencyType;
    type: EPrintingEditionType;
    price: number;
    authors: string[];
    description: string;
}