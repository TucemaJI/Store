import { IAuthor } from "../../shared/models/IAuthor";
import { IClients } from "../../shared/models/IClients";
import { IClientsPageModel } from "../../shared/models/IClientsPageModel";

export interface IAdministratorState {
    clients: IClients[];
    authors: IAuthor[];
    pageModel: IClientsPageModel;
}
export const initialAdministratorState = {
    clients: null,
    authors: null,
    pageModel: null,
}