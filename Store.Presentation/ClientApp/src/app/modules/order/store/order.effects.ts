import { Injectable } from "@angular/core"
import { Actions, createEffect, ofType } from "@ngrx/effects"
import { of } from "rxjs"
import { catchError, exhaustMap, map } from "rxjs/operators"
import { error } from "src/app/store/actions/error.action"
import { IOrder } from "../../shared/models/IOrder.model"
import { IOrderPage } from "../../shared/models/IOrderPage.model"
import { IPageOptions } from "../../shared/models/IPageOptions.model"
import { OrderHttpService } from "../../shared/services/order-http.service"
import { EOrderActions } from "./order.action"

@Injectable()
export class OrderEffects {
    getOrders$ = createEffect(() => this.actions$.pipe(
        ofType(EOrderActions.GetOrders),
        exhaustMap((action: { type: string, pageModel: IOrderPage }) =>
            this.httpService.postGetOrders(action.pageModel)
                .pipe(
                    map((responce: { elements: IOrder[], pageParameters: IPageOptions }) => ({
                        type: EOrderActions.GetOrdersSuccess,
                        pageParameters: responce.pageParameters, orders: responce.elements
                    })),
                    catchError(err => of(error({ err })))
                )
        )

    ))

    constructor(
        private actions$: Actions,
        private httpService: OrderHttpService,
    ) { }
}