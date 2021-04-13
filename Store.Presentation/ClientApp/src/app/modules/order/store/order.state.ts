import { Action, Selector, State, StateContext } from "@ngxs/store";
import { tap } from "rxjs/operators";
import { IOrder } from "../../shared/models/IOrder.model";
import { IOrderPage } from "../../shared/models/IOrderPage.model";
import { OrderHttpService } from "../../shared/services/order-http.service";
import { GetOrders } from "./order.action";

export interface IOrderState {
    orders: IOrder[];
    pageModel: IOrderPage;
}

@State<IOrderState>({
    name: 'order',
    defaults: {
        orders: [],
        pageModel: {
            isDescending: null,
            orderByString: null,
            status: null,
            userId: null,
            pageOptions: null
        },
    }
})

export class OrderState {
    constructor(private httpService: OrderHttpService) { }

    @Selector()
    static getState(state: IOrderState) {
        return state;
    }

    @Action(GetOrders)
    getOrders({ getState, setState }: StateContext<IOrderState>, pageModel: IOrderPage) {
        return this.httpService.postGetOrders(pageModel).pipe(
            tap(result => {
                const state = getState();
                setState({
                    ...state,
                    orders: result.elements,
                    ...state.pageModel.pageOptions = result.pageOptions,
                });
            })
        );
    }
}