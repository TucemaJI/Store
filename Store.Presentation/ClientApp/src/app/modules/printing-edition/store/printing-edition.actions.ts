import { createAction, props } from "@ngrx/store";
import { IPageParameters } from "../../shared/models/IPageParameters.model";
import { IPrintingEdition } from "../../shared/models/IPrintingEdition.model";
import { IPrintingEditionPage } from "../../shared/models/IPEPage.model";

export enum EPrintingEditionActions {
    GetPEs = '[PrintingEdition] Get Printing Editions',
    GetPEsSuccess = '[PrintingEdition] Get Printing Editions Success',
    GetPE = '[PrintingEdition] Get Printing Edition',
    GetPESuccess = '[PrintingEdition] Get Printing Edition Success',
}

export const getPEs = createAction(EPrintingEditionActions.GetPEs, props<{ pageModel: IPrintingEditionPage }>());
export const getPEsSuccess = createAction(EPrintingEditionActions.GetPEsSuccess, props<{ minPrice: number, maxPrice: number, pageParameters: IPageParameters, printingEditions: IPrintingEdition[] }>());
export const getPE = createAction(EPrintingEditionActions.GetPE, props<{ id: number }>());
export const getPESuccess = createAction(EPrintingEditionActions.GetPESuccess, props<{ printingEdition: IPrintingEdition }>());