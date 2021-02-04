import { RouterReducerState } from "@ngrx/router-store";
import { initialUserState, IUserState } from "src/app/modules/account/store/account.state";

export interface IAppState {
    router?: RouterReducerState;
    user: IUserState;

}

export const initialAppState: IAppState = {
    user: initialUserState,
}

export function getInitialState(): IAppState {
    return initialAppState;
}