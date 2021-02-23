import { createAction, props } from "@ngrx/store";
import { IAuthor } from "../../shared/models/IAuthor";
import { IAuthorsPageModel } from "../../shared/models/IAuthorsPageModel";
import { IChangeClientModel } from "../../shared/models/IChangeClientModel";
import { IClients } from "../../shared/models/IClients";
import { IClientsPageModel } from "../../shared/models/IClientsPageModel";
import { IPageParameters } from "../../shared/models/IPageParameters";

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
    DeleteAuthorSuccess = '[Administrator] Delete Administrator Success',
}

export const getClients = createAction(EAdministratorActions.GetClients, props<{ pageModel: IClientsPageModel }>());
export const getClientsSuccess = createAction(EAdministratorActions.GetClientsSuccess, props<{ pageParameters: IPageParameters, clients: IClients[] }>());
export const clientChange = createAction(EAdministratorActions.ClientChange, props<{ client: IChangeClientModel }>());
export const clientChangeSuccess = createAction(EAdministratorActions.ClientChangeSuccess, props<{ result: boolean }>());
export const deleteClient = createAction(EAdministratorActions.DeleteClient, props<{ client: IClients }>());
export const deleteClientSuccess = createAction(EAdministratorActions.DeleteClientSuccess, props<{ result: boolean }>());

export const getAuthors = createAction(EAdministratorActions.GetAuthors, props<{ pageModel: IAuthorsPageModel }>());
export const getAuthorsSuccess = createAction(EAdministratorActions.GetAuthorsSuccess, props<{ pageParameters: IPageParameters, authors: IAuthor[] }>());
export const addAuthor = createAction(EAdministratorActions.AddAuthor, props<{ author: IAuthor }>());
export const addAuthorSuccess = createAction(EAdministratorActions.AddAuthorSuccess, props<{ result }>());
export const changeAuthor = createAction(EAdministratorActions.ChangeAuthor, props<{author:IAuthor}>());
export const changeAuthorSuccess = createAction(EAdministratorActions.ChangeAuthorSuccess, props<{result}>());
export const deleteAuthor = createAction(EAdministratorActions.DeleteAuthor, props<{author:IAuthor}>());
export const deleteAuthorSuccess = createAction(EAdministratorActions.DeleteAuthorSuccess, props<{result}>());