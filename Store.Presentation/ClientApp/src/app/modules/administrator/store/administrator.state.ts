import { IClients } from "../models/IClients";
import { IClientsPageModel } from "../models/IClientsPageModel";

export interface IAdministratorState {
    clients: IClients[];
    pageModel: IClientsPageModel;
}
export const initialAdministratorState = {
    clients: null,
    pageModel: null,
}