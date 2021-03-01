import { ECurrencyType } from "./ECurrencyType";
import { EPrintingEditionType } from "./EPrintingEditionType";

export interface IPrintingEdition{
    title:string;
    currencyType:ECurrencyType;
    type:EPrintingEditionType;
    price:number;
    authors:string[];
}