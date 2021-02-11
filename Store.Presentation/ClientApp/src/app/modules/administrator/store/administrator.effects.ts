import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { of } from "rxjs";
import { catchError, exhaustMap, map } from "rxjs/operators";
import { error } from "src/app/store/actions/error.action";
import { IClients } from "../models/IClients";
import { IPageModel } from "../models/IPageModel";
import { AdministratorHttpService } from "../services/http.service";
import { EAdministratorActions } from "./administrator.actions";

@Injectable()
export class AdministratorEffects {
    getUsers$ = createEffect(() => this.actions$.pipe(
        ofType(EAdministratorActions.GetClients),
        exhaustMap((pageModel: IPageModel) => this.httpService.postPage(pageModel)
            .pipe(
                map((responce: IClients) => { debugger; return ({ type: EAdministratorActions.GetClientsSuccess, clients: responce }) }),
                catchError(err => of(error({ err })))
            )
        )
    ))

    constructor(
        private actions$: Actions,
        private httpService: AdministratorHttpService,
    ) { }
}