import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { of } from "rxjs";
import { catchError, exhaustMap, map } from "rxjs/operators";
import { error } from "src/app/store/actions/error.action";
import { IAuthor } from "../../shared/models/IAuthor";
import { IAuthorsPageModel } from "../../shared/models/IAuthorsPageModel";
import { IChangeClientModel } from "../../shared/models/IChangeClientModel";
import { IClients } from "../../shared/models/IClients";
import { IClientsPageModel } from "../../shared/models/IClientsPageModel";
import { IPageParameters } from "../../shared/models/IPageParameters";
import { AdministratorHttpService } from "../../shared/services/administrator-http.service";
import { EAdministratorActions } from "./administrator.actions";

@Injectable()
export class AdministratorEffects {
    getUsers$ = createEffect(() => this.actions$.pipe(
        ofType(EAdministratorActions.GetClients),
        exhaustMap((pageModel: IClientsPageModel) => this.httpService.postClientsPage(pageModel)
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

    getAuthors$ = createEffect(()=> this.actions$.pipe(
        ofType(EAdministratorActions.GetAuthors),
        exhaustMap((pageModel: IAuthorsPageModel) => this.httpService.postAuthorPage(pageModel)
            .pipe(
                map((responce: { elements: IAuthor[], pageParameters: IPageParameters }) => ({
                    type: EAdministratorActions.GetAuthorsSuccess,
                    pageParameters: responce.pageParameters, authors: responce.elements
                })),
                catchError(err => of(error({ err })))
            )
        )
    ))
    addAuthor$ = createEffect(()=> this.actions$.pipe(
        ofType(EAdministratorActions.AddAuthor),
        exhaustMap((author:IAuthor) => this.httpService.addAuthor(author)
            .pipe(
                map((response: boolean) => ({ type: EAdministratorActions.AddAuthorSuccess, result: response })),
                catchError(err => of(error({ err })))

            )
        )
    ))
    changeAuthor$ = createEffect(()=> this.actions$.pipe(
        ofType(EAdministratorActions.ChangeAuthor),
        exhaustMap((author:IAuthor) => this.httpService.changeAuthor(author)
            .pipe(
                map((response: boolean) => ({ type: EAdministratorActions.ChangeAuthorSuccess, result: response })),
                catchError(err => of(error({ err })))

            )
        )
    ))
    deleteAuthor$ = createEffect(()=> this.actions$.pipe(
        ofType(EAdministratorActions.DeleteAuthor),
        exhaustMap((author:IAuthor) => this.httpService.deleteAuthor(author)
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