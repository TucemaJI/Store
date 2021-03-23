import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Consts } from "../consts";
import { IAuthorsPage } from "../models/IAuthorsPage.model";
import { IClientsPage } from "../models/IClientsPage.model";

@Injectable()
export class AdministratorHttpService {

    constructor(private http: HttpClient) { }

    postClientsPage(pageModel: IClientsPage) {
        const body = {
            entityParameters: { itemsPerPage: pageModel.pageParameters.itemsPerPage, currentPage: pageModel.pageParameters.currentPage },
            isDescending: pageModel.isDescending, orderByString: pageModel.orderByString,
            name: pageModel.name, email: pageModel.email, isBlocked: pageModel.isBlocked,
        };
        debugger;
        return this.http.post(Consts.FILTER_USERS, body);
    }
    changeClient(client) {
        const body = {
            firstName: client.client.firstName,
            lastName: client.client.lastName,
            email: client.client.email,
            isBlocked: client.client.isBlocked,
            password: client.client.password,
            confirmPassword: client.client.confirmPassword,
            id: client.client.id,
        }
        return this.http.put(Consts.UPDATE_USER, body);
    }
    deleteClient(client) {
        console.log(client);
        debugger;
        const body = {
            firstName: client.client.firstName,
            lastName: client.client.lastName,
            email: client.client.email,
            isBlocked: client.client.isBlocked
        }
        return this.http.post(Consts.DELETE_USER, body);
    }

    postAuthorPage(pageModel: IAuthorsPage) {
        const body = {
            entityParameters: { itemsPerPage: pageModel.pageParameters.itemsPerPage, currentPage: pageModel.pageParameters.currentPage },
            isDescending: pageModel.isDescending, orderByString: pageModel.orderByString,
            name: pageModel.name, id: pageModel.id,
        };
        debugger;
        return this.http.post(Consts.GET_AUTHORS, body);
    }
    addAuthor(author) {
        const body = {
            firstName: author.author.firstName,
            lastName: author.author.lastName,
        };
        debugger;
        return this.http.post(Consts.CREATE_AUTHOR, body);
    }
    changeAuthor(author) {
        const body = {
            id: author.author.id,
            firstName: author.author.firstName,
            lastName: author.author.lastName,
            printingEditions: author.author.printingEditions,
        }
        return this.http.post(Consts.UPDATE_AUTHOR, body);
    }
    deleteAuthor(author) {
        const body = {
            id: author.author.id,
            firstName: author.author.firstName,
            lastName: author.author.lastName,
        };
        return this.http.post(Consts.DELETE_AUTHOR, body);
    }

}

