import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { error } from "../../../store/actions/error.action";
import { of } from "rxjs";
import { catchError, exhaustMap, map } from "rxjs/operators";
import { IOrder } from "../../shared/models/IOrder.model";
import { OrderHttpService } from "../../shared/services/order-http.service";
import { ECartActions } from "./cart.actions";
import { IPay } from "../../shared/models/IPay.model";
import { ICreateOrder } from "../../shared/models/ICreateOrder.model";

@Injectable()
export class CartEffects {
    createOrder$ = createEffect(() => this.actions$.pipe(
        ofType(ECartActions.CreateOrder),
        exhaustMap((orderAction: { type: string, order: ICreateOrder }) =>
            this.httpService.postCreateOrder(orderAction.order)
                .pipe(
                    map((responce: Number) => ({
                        type: ECartActions.CreateOrderSuccess,
                        orderId: responce
                    })),
                    catchError(err => of(error({ err })))
                )
        )

    ))

    pay$ = createEffect(() => this.actions$.pipe(
        ofType(ECartActions.Pay),
        exhaustMap((payAction: { type: string, payment: IPay }) =>
            this.httpService.postPay(payAction.payment)
                .pipe(
                    map((responce: boolean) => ({
                        type: ECartActions.PaySuccess,
                        result: responce
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