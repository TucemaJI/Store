import { createAction, props } from "@ngrx/store";
import { IChangeClientModel } from "../models/IChangeClientModel";
import { IClients } from "../models/IClients";
import { IClientsPageModel } from "../models/IClientsPageModel";
import { IPageParameters } from "../models/IPageParameters";

export enum EAdministratorActions {
    GetClients = '[Administrator] Get Clients',
    GetClientsSuccess = '[Administrator] Get Clients Success',
    ClientChange = '[Administrator] Client Change',
    ClientChangeSuccess = '[Administrator] Client Change Success',
    DeleteClient = '[Administrator] Delete Client',
    DeleteClientSuccess = '[Administrator] Delete Client Success',
}

export const getClients = createAction(EAdministratorActions.GetClients, props<{ pageModel: IClientsPageModel }>());
export const getClientsSuccess = createAction(EAdministratorActions.GetClientsSuccess, props<{ pageParameters: IPageParameters, clients:IClients[] }>());
export const clientChange = createAction(EAdministratorActions.ClientChange, props<{ client: IChangeClientModel }>());
export const clientChangeSuccess = createAction(EAdministratorActions.ClientChangeSuccess, props<{ result: boolean }>());
export const deleteClient = createAction(EAdministratorActions.DeleteClient, props<{ client: IClients }>());
export const deleteClientSuccess = createAction(EAdministratorActions.DeleteClientSuccess, props<{ result: boolean }>())