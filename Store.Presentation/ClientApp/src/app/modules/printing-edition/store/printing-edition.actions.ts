import { createAction, props } from "@ngrx/store";
import { IPageParameters } from "../../shared/models/IPageParameters";
import { IPrintingEdition } from "../../shared/models/IPrintingEdition";
import { IPrintingEditionPageModel } from "../../shared/models/IPrintingEditionPageModel";

export enum EPrintingEditionActions {
    GetPE = '[PrintingEdition] Get Printing Editions',
    GetPESuccess = '[PrintingEdition] Get Printing Editions Success',
    GetMaxPrice = '[PrintingEdition] Get Max Price',
    GetMaxPriceSuccess = '[PrintingEdition] Get Max Price Success',
}

export const getPE = createAction(EPrintingEditionActions.GetPE, props<{ pageModel: IPrintingEditionPageModel }>());
export const getPESuccess = createAction(EPrintingEditionActions.GetPESuccess, props<{ maxPrice: number, pageParameters: IPageParameters, printingEditions: IPrintingEdition[] }>());
export const getMaxPrice = createAction(EPrintingEditionActions.GetMaxPrice);
export const getMaxPriceSuccess = createAction(EPrintingEditionActions.GetMaxPriceSuccess, props<{ maxPrice: number }>());