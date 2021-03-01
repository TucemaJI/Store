import { createSelector } from "@ngrx/store";
import { IAppState } from "src/app/store/state/app.state";
import { IPrintingEditionState } from "./printing-edition.state";

const getState = (state: IAppState) => state.printingEditions;

export const selectPrintingEditions = createSelector(
    getState,
    (state: IPrintingEditionState) => state,
)