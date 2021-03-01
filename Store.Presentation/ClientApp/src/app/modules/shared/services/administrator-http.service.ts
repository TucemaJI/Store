import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { IAuthor } from "../models/IAuthor";
import { IAuthorsPageModel } from "../models/IAuthorsPageModel";
import { IClients } from "../models/IClients";
import { IClientsPageModel } from "../models/IClientsPageModel";

@Injectable()
export class AdministratorHttpService {

    constructor(private http: HttpClient) { }

    postClientsPage(pageModel: IClientsPageModel) {
        const body = {
            entityParameters: { itemsPerPage: pageModel.pageParameters.itemsPerPage, currentPage: pageModel.pageParameters.currentPage },
            isDescending: pageModel.isDescending, orderByString: pageModel.orderByString,
            name: pageModel.name, email: pageModel.email, isBlocked: pageModel.isBlocked,
        };
        debugger;
        return this.http.post('https://localhost:44355/api/user/filterusers', body);
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
        return this.http.put('https://localhost:44355/api/user/updateuser', body);
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
        return this.http.post('https://localhost:44355/api/user/deleteuser', body);
    }

    postAuthorPage(pageModel: IAuthorsPageModel) {
        const body = {
            entityParameters: { itemsPerPage: pageModel.pageParameters.itemsPerPage, currentPage: pageModel.pageParameters.currentPage },
            isDescending: pageModel.isDescending, orderByString: pageModel.orderByString,
            name: pageModel.name, id: pageModel.id,
        };
        debugger;
        return this.http.post('https://localhost:44355/api/author/getauthorswithfilter', body);
    }
    addAuthor(author) {
        const body = {
            firstName: author.author.firstName,
            lastName: author.author.lastName,
        };
        debugger;
        return this.http.post('https://localhost:44355/api/author/createauthor', body);
    }
    changeAuthor(author) {
        const body = {
            id: author.author.id,
            firstName: author.author.firstName,
            lastName: author.author.lastName,
            printingEditions: author.author.printingEditions,
        }
        return this.http.post('https://localhost:44355/api/author/updateauthor', body);
    }
    deleteAuthor(author) {
        const body = {
            id: author.author.id,
            firstName: author.author.firstName,
            lastName: author.author.lastName,
        };
        return this.http.post('https://localhost:44355/api/author/deleteauthor', body);
    }

}

