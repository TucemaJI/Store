import { Action, createReducer, on } from "@ngrx/store";
import * as AdministratorActions from "./administrator.actions";
import { IAdministratorState, initialAdministratorState } from "./administrator.state";

const createAdministratorReducer = createReducer(
    initialAdministratorState,
    on(AdministratorActions.getClientsSuccess, (state, { clients }) => { debugger; return ({ ...state, clients : clients }) }),
    //on(AdministratorActions.clientChangeSuccess,(state, {result})=> ({...state.clients,}))
)

export const administratorReducer = (state = initialAdministratorState, action: Action): IAdministratorState => {
    return createAdministratorReducer(state, action);
}
export const reducerKey = 'administrator';