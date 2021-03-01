import { createAction, props } from "@ngrx/store";
import { IPageParameters } from "../../shared/models/IPageParameters";
import { IPrintingEdition } from "../../shared/models/IPrintingEdition";
import { IPrintingEditionPageModel } from "../../shared/models/IPrintingEditionPageModel";

export enum EPrintingEditionActions {
    GetPE = '[PrintingEdition] Get Printing Editions',
    GetPESuccess = '[PrintingEdition] Get Printing Editions Success',
}

export const getPE = createAction(EPrintingEditionActions.GetPE, props<{ pageModel: IPrintingEditionPageModel }>());
export const getPESuccess = createAction(EPrintingEditionActions.GetPESuccess, props<{ pageParameters: IPageParameters, printingEditions: IPrintingEdition[] }>());