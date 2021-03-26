import { IPrintingEdition } from "../../shared/models/IPrintingEdition.model";
import { IPrintingEditionPage } from "../../shared/models/IPrintingEditionPage.model";

export interface IPrintingEditionState {
    printingEditions: IPrintingEdition[];
    pageModel: IPrintingEditionPage;
}
export const initialPrintingEditionsState = {
    printingEditions: null,
    pageModel: null,
}