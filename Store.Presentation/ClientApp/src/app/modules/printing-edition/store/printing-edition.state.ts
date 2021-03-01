import { IPrintingEdition } from "../../shared/models/IPrintingEdition";
import { IPrintingEditionPageModel } from "../../shared/models/IPrintingEditionPageModel";

export interface IPrintingEditionState {
    printingEditions: IPrintingEdition[];
    pageModel: IPrintingEditionPageModel;
}
export const initialPrintingEditionsState = {
    printingEditions: null,
    pageModel: null,
}