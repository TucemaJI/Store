import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { error } from "../../../store/actions/error.action";
import { of } from "rxjs";
import { catchError, exhaustMap, map } from "rxjs/operators";
import { IOrderModel } from "../../shared/models/IOrderModel";
import { OrderHttpService } from "../../shared/services/order-http.service";
import { ECartActions } from "./cart.actions";

@Injectable()
export class CartEffects {
    createOrder$ = createEffect(() => this.actions$.pipe(
        ofType(ECartActions.CreateOrder),
        exhaustMap((orderAction: {type:string, order:IOrderModel}) => {
            debugger; return (
                this.httpService.postCreateOrder(orderAction.order)
                    .pipe(
                        map((responce: Number) => ({
                            type: ECartActions.CreateOrderSuccess,
                            orderId: responce
                        })),
                        catchError(err => of(error({ err })))
                    )
            )
        }
        )
    ))

    constructor(
        private actions$: Actions,
        private httpService: OrderHttpService,
    ) { }
}