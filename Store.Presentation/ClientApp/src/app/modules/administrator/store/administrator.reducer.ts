import { Action, createReducer, on } from "@ngrx/store";
import * as AdministratorActions from "./administrator.actions";
import { IAdministratorState, initialAdministratorState } from "./administrator.state";

const createAdministratorReducer = createReducer(
    initialAdministratorState,
    on(AdministratorActions.getClientsSuccess, (state, { pageOptions, clients }) => ({ ...state, clients: clients, pageModel: { ...state.pageModel, pageOptions: pageOptions, } })),
    on(AdministratorActions.getAuthorsSuccess, (state, { pageOptions, authors }) => ({ ...state, authors: authors, pageModel: { ...state.pageModel, pageOptions: pageOptions, } })),
    
)

export const administratorReducer = (state = initialAdministratorState, action: Action): IAdministratorState => {
    return createAdministratorReducer(state, action);
}

export const reducerKey = 'administrator';