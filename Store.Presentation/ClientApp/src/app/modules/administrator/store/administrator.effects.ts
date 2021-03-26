import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { of } from "rxjs";
import { catchError, exhaustMap, map } from "rxjs/operators";
import { error } from "src/app/store/actions/error.action";
import { IAuthor } from "../../shared/models/IAuthor.model";
import { IAuthorsPage } from "../../shared/models/IAuthorsPage.model";
import { IChangeClient } from "../../shared/models/IChangeClient.model";
import { IClient } from "../../shared/models/IClient.model";
import { IClientsPage } from "../../shared/models/IClientsPage.model";
import { IPageOptions } from "../../shared/models/IPageOptions.model";
import { AdministratorHttpService } from "../../shared/services/administrator-http.service";
import { EAdministratorActions } from "./administrator.actions";

@Injectable()
export class AdministratorEffects {
    getUsers$ = createEffect(() => this.actions$.pipe(
        ofType(EAdministratorActions.GetClients),
        exhaustMap((action: { pageModel: IClientsPage }) => this.httpService.postClientsPage(action.pageModel)
            .pipe(
                map((responce: { elements: IClient[], pageOptions: IPageOptions }) => ({
                    type: EAdministratorActions.GetClientsSuccess,
                    pageOptions: responce.pageOptions, clients: responce.elements
                })),
                catchError(err => of(error({ err })))
            )
        )
    ))
    changeClient$ = createEffect(() => this.actions$.pipe(
        ofType(EAdministratorActions.ClientChange),
        exhaustMap((action: { client: IChangeClient }) => this.httpService.changeClient(action.client)
            .pipe(
                map((response: boolean) => ({ type: EAdministratorActions.ClientChangeSuccess, result: response })),
                catchError(err => of(error({ err })))

            )
        )
    ))
    deleteClient$ = createEffect(() => this.actions$.pipe(
        ofType(EAdministratorActions.DeleteClient),
        exhaustMap((action: { client: IClient }) => this.httpService.deleteClient(action.client)
            .pipe(
                map((response) => ({ type: EAdministratorActions.DeleteClientSuccess, result: response })),
                catchError(err => of(error({ err })))
            )
        )
    ))

    getAuthors$ = createEffect(() => this.actions$.pipe(
        ofType(EAdministratorActions.GetAuthors),
        exhaustMap((action: { pageModel: IAuthorsPage }) => this.httpService.postAuthorPage(action.pageModel)
            .pipe(
                map((responce: { elements: IAuthor[], pageOptions: IPageOptions }) => ({
                    type: EAdministratorActions.GetAuthorsSuccess,
                    pageOptions: responce.pageOptions, authors: responce.elements
                })),
                catchError(err => of(error({ err })))
            )
        )
    ))
    addAuthor$ = createEffect(() => this.actions$.pipe(
        ofType(EAdministratorActions.AddAuthor),
        exhaustMap((action: { author: IAuthor }) => this.httpService.addAuthor(action.author)
            .pipe(
                map((response: boolean) => ({ type: EAdministratorActions.AddAuthorSuccess, result: response })),
                catchError(err => of(error({ err })))

            )
        )
    ))
    changeAuthor$ = createEffect(() => this.actions$.pipe(
        ofType(EAdministratorActions.ChangeAuthor),
        exhaustMap((action: { author: IAuthor }) => this.httpService.changeAuthor(action.author)
            .pipe(
                map((response: boolean) => ({ type: EAdministratorActions.ChangeAuthorSuccess, result: response })),
                catchError(err => of(error({ err })))

            )
        )
    ))
    deleteAuthor$ = createEffect(() => this.actions$.pipe(
        ofType(EAdministratorActions.DeleteAuthor),
        exhaustMap((action: { author: IAuthor }) => this.httpService.deleteAuthor(action.author)
            .pipe(
                map((response: boolean) => ({ type: EAdministratorActions.DeleteAuthorSuccess, result: response })),
                catchError(err => of(error({ err })))

            )
        )
    ))

    constructor(
        private actions$: Actions,
        private httpService: AdministratorHttpService,
    ) { }
}