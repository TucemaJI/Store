import { Injectable } from "@angular/core";
import { Action, Selector, State, StateContext } from "@ngxs/store"
import { tap } from "rxjs/operators";
import { IOrderState } from "../../order/store/order.state";
import { EStatusType } from "../../shared/enums/status-type.enum";
import { ICreateOrder } from "../../shared/models/ICreateOrder.model";
import { IPay } from "../../shared/models/IPay.model";
import { OrderHttpService } from "../../shared/services/order-http.service"
import { CreateOrder, Pay } from "./cart.actions";

export interface ICartState {
    orderId: number,
    orderStatus: boolean,
}

@State<ICartState>({
    name: 'cart',
    defaults: {
        orderId: null,
        orderStatus: null,
    }
})

@Injectable()
export class CartState {
    constructor(private httpService: OrderHttpService) { }

    @Selector()
    static createOrder(state: ICartState) {
        return state;
    }

    @Action(CreateOrder)
    createOrder({ getState, setState }: StateContext<ICartState>, payload: { order: ICreateOrder }) {
        return this.httpService.postCreateOrder(payload.order).pipe(
            tap(result => {
                const state = getState();
                setState({
                    ...state,
                    orderId: result
                });
            })
        );
    }

    @Action(Pay)
    pay(cartState: StateContext<ICartState>, payload: { payment: IPay, orderId: number }) {
        return this.httpService.postPay(payload.payment).pipe(
            tap(result => {
                const cState = cartState.getState();
                cartState.setState({
                    ...cState,
                    orderStatus: result
                });
            })
        );
    }
}