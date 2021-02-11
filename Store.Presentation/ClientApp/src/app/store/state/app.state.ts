import { RouterReducerState } from "@ngrx/router-store";
import { initialUserState, IAccountState } from "src/app/modules/account/store/account.state";
import { IAdministratorState, initialAdministratorState } from "src/app/modules/administrator/store/administrator.state";

export interface IAppState {
    router?: RouterReducerState;
    account: IAccountState;
    administrator: IAdministratorState;
}

export const initialAppState: IAppState = {
    account: initialUserState,
    administrator: initialAdministratorState,
}

export function getInitialState(): IAppState {
    return initialAppState;
}