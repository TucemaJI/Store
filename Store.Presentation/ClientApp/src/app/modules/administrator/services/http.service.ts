import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { IClients } from "../models/IClients";
import { IPageModel } from "../models/IPageModel";

@Injectable()
export class AdministratorHttpService {

    constructor(private http: HttpClient) { }

    postPage(pageModel: IPageModel) {
        const body = {
            entityParameters: { pageNumber: pageModel.pageParameters.pageNumber, pageSize: pageModel.pageParameters.pageSize },
            isDescending: pageModel.isDescending, orderByString: pageModel.orderByString,
            name: pageModel.name, email: pageModel.email,
        };
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

}

