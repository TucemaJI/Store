import { createAction, props } from "@ngrx/store";
import { IClients } from "../models/IClients";
import { IPageModel } from "../models/IPageModel";

export enum EAdministratorActions {
    GetClients = '[Administrator] Get Clients',
    GetClientsSuccess = '[Administrator] Get Clients Success',
}

export const getClients = createAction(EAdministratorActions.GetClients, props<{ pageModel: IPageModel }>());
export const getClientsSuccess = createAction(EAdministratorActions.GetClientsSuccess, props<{ clients: IClients }>());