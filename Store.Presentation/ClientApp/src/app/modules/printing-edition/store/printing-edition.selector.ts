import { createSelector } from "@ngrx/store";
import { IAppState } from "src/app/store/state/app.state";
import { IAccountState } from "../../account/store/account.state";
import { IPrintingEditionState } from "./printing-edition.state";

const getPEState = (state: IAppState) => state.printingEditions;
const getUserState = (state: IAppState) => state.account;

export const selectUser = createSelector(
    getUserState,
    (state: IAccountState) => state.user,
)
export const selectPrintingEditions = createSelector(
    getPEState,
    (state: IPrintingEditionState) => state,
)