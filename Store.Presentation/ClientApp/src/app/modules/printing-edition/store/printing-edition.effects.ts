import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { of } from "rxjs";
import { catchError, exhaustMap, map } from "rxjs/operators";
import { error } from "src/app/store/actions/error.action";
import { IPageParameters } from "../../shared/models/IPageParameters";
import { IPrintingEdition } from "../../shared/models/IPrintingEdition";
import { IPrintingEditionPageModel } from "../../shared/models/IPrintingEditionPageModel";
import { PrintingEditionHttpService } from "../../shared/services/printing-edition-http.service";
import { EPrintingEditionActions } from "./printing-edition.actions";

@Injectable()
export class PrintingEditionEffects {
    getPE$ = createEffect(() => this.actions$.pipe(
        ofType(EPrintingEditionActions.GetPE),
        exhaustMap((pageModel: IPrintingEditionPageModel) => {
            debugger;
            return (
                this.httpService.postPE(pageModel)
                    .pipe(
                        map((responce: { elements: IPrintingEdition[], pageParameters: IPageParameters, maxPrice: number }) => {
                            debugger; return ({
                                type: EPrintingEditionActions.GetPESuccess,
                                pageParameters: responce.pageParameters, printingEditions: responce.elements, maxPrice: responce.maxPrice,
                            })
                        }),
                        catchError(err => of(error({ err })))
                    ))
        }
        )
    ))
    getMaxPrice$ = createEffect(() => this.actions$.pipe(
        ofType(EPrintingEditionActions.GetMaxPrice),
        exhaustMap(() => {
            debugger;
            return (
                this.httpService.getMaxPrice()
                    .pipe(
                        map((responce: { maxPrice: number }) => {
                            debugger; return ({
                                type: EPrintingEditionActions.GetMaxPriceSuccess,
                                maxPrice: responce.maxPrice,
                            })
                        }),
                        catchError(err => of(error({ err })))
                    ))
        }
        )
    ))
    constructor(
        private actions$: Actions,
        private httpService: PrintingEditionHttpService,
    ) { }
}