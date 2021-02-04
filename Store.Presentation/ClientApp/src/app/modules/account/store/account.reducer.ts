import { Action, createReducer, on } from "@ngrx/store";
import * as AccountActions from "./account.actions";
import { initialUserState, IUserState } from "./account.state";

export interface State {

}
const accountReducer = createReducer(
    initialUserState,
    on(AccountActions.signInSuccess, state => ({ ...state })),
    on(AccountActions.signUpSuccess, state => ({ ...state })),
    on(AccountActions.passwordRecoverySuccess, state => ({ ...state })),
)

export const reducer = (state = initialUserState, action: Action): IUserState => {
    return accountReducer(state, action);
}
export const reducerKey = 'account';