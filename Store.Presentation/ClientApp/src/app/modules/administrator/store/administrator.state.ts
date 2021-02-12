import { IClients } from "../models/IClients";

export interface IAdministratorState {
    clients: IClients[];
}
export const initialAdministratorState = {
    clients: null,
}