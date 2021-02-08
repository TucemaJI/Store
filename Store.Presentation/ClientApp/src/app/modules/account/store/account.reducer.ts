import { state } from "@angular/animations";
import { Action, createReducer, on } from "@ngrx/store";
import * as AccountActions from "./account.actions";
import { initialUserState, IAccountState } from "./account.state";

const createAccountReducer = createReducer(
    initialUserState,
    on(AccountActions.signInSuccess, (state, {accessToken, refreshToken}) => {debugger; return({ ...state, user: {...state.user, accessToken, refreshToken} })}),
    on(AccountActions.signUpSuccess, state => ({ ...state,  })),
    on(AccountActions.passwordRecoverySuccess, state => ({ ...state })),
)

export const accountReducer = (state = initialUserState, action: Action): IAccountState => {
    return createAccountReducer(state, action);
}
export const reducerKey = 'account';