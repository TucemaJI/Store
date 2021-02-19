import { createSelector } from "@ngrx/store";
import { IAppState } from "src/app/store/state/app.state";
import { IAdministratorState } from "./administrator.state";

const getClients = (state: IAppState) => state.administrator;

export const selectAdministrator = createSelector(
    getClients,
    (state: IAdministratorState) => state,
)