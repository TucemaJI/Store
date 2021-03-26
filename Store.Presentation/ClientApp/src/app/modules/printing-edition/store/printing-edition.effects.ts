import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { of } from "rxjs";
import { catchError, exhaustMap, map } from "rxjs/operators";
import { error } from "src/app/store/actions/error.action";
import { IPageOptions } from "../../shared/models/IPageOptions.model";
import { IPrintingEdition } from "../../shared/models/IPrintingEdition.model";
import { IPrintingEditionPage } from "../../shared/models/IPrintingEditionPage.model";
import { PrintingEditionHttpService } from "../../shared/services/printing-edition-http.service";
import { EPrintingEditionActions } from "./printing-edition.actions";

@Injectable()
export class PrintingEditionEffects {
    getPEs$ = createEffect(() => this.actions$.pipe(
        ofType(EPrintingEditionActions.GetPEs),
        exhaustMap((action: { pageModel: IPrintingEditionPage }) =>
            this.httpService.postPE(action.pageModel)
                .pipe(
                    map((responce: { elements: IPrintingEdition[], pageOptions: IPageOptions, maxPrice: number, minPrice: number }) => ({
                        type: EPrintingEditionActions.GetPEsSuccess,
                        pageOptions: responce.pageOptions, printingEditions: responce.elements, maxPrice: responce.maxPrice, minPrice: responce.minPrice
                    })
                    ),
                    catchError(err => of(error({ err })))
                )
        )
    ));

    getPE$ = createEffect(() => this.actions$.pipe(
        ofType(EPrintingEditionActions.GetPE),
        exhaustMap((action: { id: number }) =>
            this.httpService.getPE(action.id)
                .pipe(
                    map((responce: IPrintingEdition) => ({
                        type: EPrintingEditionActions.GetPESuccess,
                        printingEdition: responce,
                    })
                    ),
                    catchError(err => of(error({ err })))
                )
        )
    ))

    constructor(
        private actions$: Actions,
        private httpService: PrintingEditionHttpService,
    ) { }
}