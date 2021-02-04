import { createSelector } from "@ngrx/store";
import { IAppState } from "src/app/store/state/app.state";
import { IUserState } from "./account.state";

const signIn = (state: IAppState) => state.user;

export const selectAccount = createSelector(
    signIn,
    (state: IUserState) => state.user,
)