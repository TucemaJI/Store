import { RouterReducerState } from "@ngrx/router-store";
import { initialUserState, IAccountState } from "src/app/modules/account/store/account.state";

export interface IAppState {
    router?: RouterReducerState;
    account: IAccountState;

}

export const initialAppState: IAppState = {
    account: initialUserState,
}

export function getInitialState(): IAppState {
    return initialAppState;
}