import { IResultPageModel } from "./IResultPage.model";

export interface IResultPrintingEditionPageModel extends IResultPageModel {
    maxPrice: number;
    minPrice: number;
}