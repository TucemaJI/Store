import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Consts } from "../consts";
import { IAuthor } from "../models/IAuthor.model";
import { IAuthorsPage } from "../models/IAuthorsPage.model";
import { IClient } from "../models/IClient.model";
import { IClientsPage } from "../models/IClientsPage.model";

@Injectable()
export class AdministratorHttpService {

    constructor(private http: HttpClient) { }

    postClientsPage(pageModel: IClientsPage) {
        return this.http.post(Consts.FILTER_USERS, pageModel);
    }
    changeClient(client: IClient) {
        return this.http.put(Consts.UPDATE_USER, client);
    }
    deleteClient(client: IClient) {
        return this.http.post(Consts.DELETE_USER, client);
    }
    postAuthorPage(pageModel: IAuthorsPage) {
        return this.http.post(Consts.GET_AUTHORS, pageModel);
    }
    addAuthor(author: IAuthor) {
        return this.http.post(Consts.CREATE_AUTHOR, author);
    }
    changeAuthor(author: IAuthor) {
        return this.http.post(Consts.UPDATE_AUTHOR, author);
    }
    deleteAuthor(author: IAuthor) {
        return this.http.post(Consts.DELETE_AUTHOR, author);
    }

}

