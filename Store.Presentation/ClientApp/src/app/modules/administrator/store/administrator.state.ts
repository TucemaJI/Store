import { IAuthor } from "../../shared/models/IAuthor.model";
import { IClient } from "../../shared/models/IClient.model";
import { IClientsPage } from "../../shared/models/IClientsPage.model";

export interface IAdministratorState {
    clients: IClient[];
    authors: IAuthor[];
    pageModel: IClientsPage;
}

export const initialAdministratorState = {
    clients: null,
    authors: null,
    pageModel: null,
}