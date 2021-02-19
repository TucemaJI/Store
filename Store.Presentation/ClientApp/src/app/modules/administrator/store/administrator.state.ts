import { IClients } from "../models/IClients";
import { IPageModel } from "../models/IPageModel";

export interface IAdministratorState {
    clients: IClients[];
    pageModel: IPageModel;
}
export const initialAdministratorState = {
    clients: null,
    pageModel: null,
}