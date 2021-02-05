import { Action, createReducer, on } from "@ngrx/store";
import { Token } from "../services/HttpService";
import * as AccountActions from "./account.actions";
import { initialUserState, IUserState } from "./account.state";

const accountReducer = createReducer(
    initialUserState,
    on(AccountActions.signInSuccess, (state, {token}) => {debugger; return({ ...state, token: token })}),
    on(AccountActions.signUpSuccess, state => ({ ...state })),
    on(AccountActions.passwordRecoverySuccess, state => ({ ...state })),
)

export const reducer = (state = initialUserState, action: Action): IUserState => {
    return accountReducer(state, action);
}
export const reducerKey = 'account';