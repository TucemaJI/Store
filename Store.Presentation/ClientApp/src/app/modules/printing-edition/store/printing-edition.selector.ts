import { createSelector } from "@ngrx/store";
import { IAppState } from "src/app/store/state/app.state";
import { IPrintingEdition } from "../../shared/models/IPrintingEdition";
import { IPrintingEditionState } from "./printing-edition.state";

const getState = (state: IAppState) => state.printingEditions;

export const selectPrintingEditions = createSelector(
    getState,
    (state: IPrintingEditionState) => state,
)
// export const selectPrintingEdition = createSelector(
//     getState, (state: IPrintingEditionState, id: number) => { debugger; return state.printingEditions.find(val => val.id === id) }
// )