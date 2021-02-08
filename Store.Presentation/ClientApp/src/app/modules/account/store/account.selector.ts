import { createSelector } from "@ngrx/store";
import { IAppState } from "src/app/store/state/app.state";
import { IAccountState } from "./account.state";

const signIn = (state: IAppState) => state.account;

export const selectAccount = createSelector(
    signIn,
    (state: IAccountState) => state,
)