import { createAction, props } from "@ngrx/store";
import { IClients } from "../models/IClients";
import { IPageModel } from "../models/IPageModel";

export enum EAdministratorActions {
    GetClients = '[Administrator] Get Clients',
    GetClientsSuccess = '[Administrator] Get Clients Success',
    ClientChange = '[Administrator] Client Change',
    ClientChangeSuccess = '[Administrator] Client Change Success',
}

export const getClients = createAction(EAdministratorActions.GetClients, props<{ pageModel: IPageModel }>());
export const getClientsSuccess = createAction(EAdministratorActions.GetClientsSuccess, props<{ clients: IClients[] }>());
export const clientChange = createAction(EAdministratorActions.ClientChange, props<{ client: IClients }>());
export const clientChangeSuccess = createAction(EAdministratorActions.ClientChangeSuccess, props<{ result: boolean }>());