import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { of } from "rxjs";
import { catchError, exhaustMap, map } from "rxjs/operators";
import { error } from "src/app/store/actions/error.action";
import { IChangeClientModel } from "../models/IChangeClientModel";
import { IClients } from "../models/IClients";
import { IClientsPageModel } from "../models/IClientsPageModel";
import { IPageParameters } from "../models/IPageParameters";
import { AdministratorHttpService } from "../services/http.service";
import { EAdministratorActions } from "./administrator.actions";

@Injectable()
export class AdministratorEffects {
    getUsers$ = createEffect(() => this.actions$.pipe(
        ofType(EAdministratorActions.GetClients),
        exhaustMap((pageModel: IClientsPageModel) => this.httpService.postPage(pageModel)
            .pipe(
                map((responce: { elements: IClients[], pageParameters: IPageParameters }) => ({
                    type: EAdministratorActions.GetClientsSuccess,
                    pageParameters: responce.pageParameters, clients: responce.elements
                })),
                catchError(err => of(error({ err })))
            )
        )
    ))
    changeClient$ = createEffect(() => this.actions$.pipe(
        ofType(EAdministratorActions.ClientChange),
        exhaustMap((client: IChangeClientModel) => this.httpService.changeClient(client)
            .pipe(
                map((response: boolean) => ({ type: EAdministratorActions.ClientChangeSuccess, result: response })),
                catchError(err => of(error({ err })))

            )
        )
    ))
    deleteClient$ = createEffect(() => this.actions$.pipe(
        ofType(EAdministratorActions.DeleteClient),
        exhaustMap((client: IClients) => this.httpService.deleteClient(client)
            .pipe(
                map((response) => ({ type: EAdministratorActions.DeleteClientSuccess, result: response })),
                catchError(err => of(error({ err })))
            )
        )
    ))

    constructor(
        private actions$: Actions,
        private httpService: AdministratorHttpService,
    ) { }
}