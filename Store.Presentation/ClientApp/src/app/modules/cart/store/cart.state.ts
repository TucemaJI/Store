import { Action, Selector, State, StateContext } from "@ngxs/store"
import { tap } from "rxjs/operators";
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
        orderStatus: false
    }
})

export class CartState {
    constructor(private httpService: OrderHttpService) { }

    @Selector()
    static createOrder(state: ICartState) {
        return state;
    }

    @Action(CreateOrder)
    createOrder({ getState, setState }: StateContext<ICartState>, order: ICreateOrder) {
        return this.httpService.postCreateOrder(order).pipe(
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
    pay({ getState, setState }: StateContext<ICartState>, payment: IPay) {
        return this.httpService.postPay(payment).pipe(
            tap(result => {
                const state = getState();
                setState({
                    ...state,
                    orderStatus: result
                });
            })
        );
    }
}