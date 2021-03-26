import { createAction, props } from "@ngrx/store";
import { IAuthor } from "../../shared/models/IAuthor.model";
import { IAuthorsPage } from "../../shared/models/IAuthorsPage.model";
import { IChangeClient } from "../../shared/models/IChangeClient.model";
import { IClient } from "../../shared/models/IClient.model";
import { IClientsPage } from "../../shared/models/IClientsPage.model";
import { IPageOptions } from "../../shared/models/IPageOptions.model";

export enum EAdministratorActions {
    GetClients = '[Administrator] Get Clients',
    GetClientsSuccess = '[Administrator] Get Clients Success',
    ClientChange = '[Administrator] Client Change',
    ClientChangeSuccess = '[Administrator] Client Change Success',
    DeleteClient = '[Administrator] Delete Client',
    DeleteClientSuccess = '[Administrator] Delete Client Success',

    GetAuthors = '[Administrator] Get Authors',
    GetAuthorsSuccess = '[Administrator] Get Author Success',
    AddAuthor = '[Administrator] Add Author',
    AddAuthorSuccess = '[Administrator] Add Author Success',
    ChangeAuthor = '[Administrator] Change Author',
    ChangeAuthorSuccess = '[Administrator] Change Author Success',
    DeleteAuthor = '[Administrator] Delete Author',
    DeleteAuthorSuccess = '[Administrator] Delete Author Success',
}

export const getClients = createAction(EAdministratorActions.GetClients, props<{ pageModel: IClientsPage }>());
export const getClientsSuccess = createAction(EAdministratorActions.GetClientsSuccess, props<{ pageParameters: IPageOptions, clients: IClient[] }>());
export const clientChange = createAction(EAdministratorActions.ClientChange, props<{ client: IChangeClient }>());
export const clientChangeSuccess = createAction(EAdministratorActions.ClientChangeSuccess, props<{ result: boolean }>());
export const deleteClient = createAction(EAdministratorActions.DeleteClient, props<{ client: IClient }>());
export const deleteClientSuccess = createAction(EAdministratorActions.DeleteClientSuccess, props<{ result: boolean }>());

export const getAuthors = createAction(EAdministratorActions.GetAuthors, props<{ pageModel: IAuthorsPage }>());
export const getAuthorsSuccess = createAction(EAdministratorActions.GetAuthorsSuccess, props<{ pageParameters: IPageOptions, authors: IAuthor[] }>());
export const addAuthor = createAction(EAdministratorActions.AddAuthor, props<{ author: IAuthor }>());
export const addAuthorSuccess = createAction(EAdministratorActions.AddAuthorSuccess, props<{ result }>());
export const changeAuthor = createAction(EAdministratorActions.ChangeAuthor, props<{author:IAuthor}>());
export const changeAuthorSuccess = createAction(EAdministratorActions.ChangeAuthorSuccess, props<{result}>());
export const deleteAuthor = createAction(EAdministratorActions.DeleteAuthor, props<{author:IAuthor}>());
export const deleteAuthorSuccess = createAction(EAdministratorActions.DeleteAuthorSuccess, props<{result}>());