import { Action, createReducer, on } from "@ngrx/store";
import * as AdministratorActions from "./administrator.actions";
import { IAdministratorState, initialAdministratorState } from "./administrator.state";

const createAdministratorReducer = createReducer(
    initialAdministratorState,
    on(AdministratorActions.getClientsSuccess, (state, { pageParameters, clients }) => { debugger; return ({ ...state, clients: clients, pageModel: { ...state.pageModel, pageParameters: pageParameters, } }) }),
)

export const administratorReducer = (state = initialAdministratorState, action: Action): IAdministratorState => {
    return createAdministratorReducer(state, action);
}
export const reducerKey = 'administrator';