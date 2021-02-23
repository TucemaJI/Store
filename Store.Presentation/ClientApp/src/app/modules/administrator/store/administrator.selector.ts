import { createSelector } from "@ngrx/store";
import { IAppState } from "src/app/store/state/app.state";
import { IAdministratorState } from "./administrator.state";

const getState = (state: IAppState) => state.administrator;

export const selectAdministrator = createSelector(
    getState,
    (state: IAdministratorState) => state,
)