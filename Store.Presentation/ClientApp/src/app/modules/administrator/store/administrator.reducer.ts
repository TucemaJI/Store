import { Action, createReducer, on } from "@ngrx/store";
import * as AdministratorActions from "./administrator.actions";
import { IAdministratorState, initialAdministratorState } from "./administrator.state";

const createAdministratorReducer = createReducer(
    initialAdministratorState,
    on(AdministratorActions.getClientsSuccess, (state, { pageParameters, clients }) => ({ ...state, clients: clients, pageModel: { ...state.pageModel, pageParameters: pageParameters, } })),
    on(AdministratorActions.getAuthorsSuccess, (state, { pageParameters, authors }) => ({ ...state, authors: authors, pageModel: { ...state.pageModel, pageParameters: pageParameters, } })),
    
)

export const administratorReducer = (state = initialAdministratorState, action: Action): IAdministratorState => {
    return createAdministratorReducer(state, action);
}

export const reducerKey = 'administrator';