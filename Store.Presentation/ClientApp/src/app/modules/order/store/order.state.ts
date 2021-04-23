import { Injectable } from "@angular/core";
import { Action, Selector, State, StateContext } from "@ngxs/store";
import { Observable } from "rxjs";
import { tap } from "rxjs/operators";
import { EStatusType } from "../../shared/enums/status-type.enum";
import { IOrder } from "../../shared/models/IOrder.model";
import { IOrderPage } from "../../shared/models/IOrderPage.model";
import { IResultPageModel } from "../../shared/models/IResultPage.model";
import { OrderHttpService } from "../../shared/services/order-http.service";
import { GetOrders, UpdateOrder } from "./order.action";

export interface IOrderState {
    orders: IOrder[];
    pageModel: IOrderPage;
}

@State<IOrderState>({
    name: 'order',
    defaults: {
        orders: null,
        pageModel: {
            isDescending: null,
            orderByString: null,
            status: null,
            userId: null,
            pageOptions: null
        },
    }
})

@Injectable()
export class OrderState {
    constructor(private httpService: OrderHttpService) { }

    @Selector()
    static getState(state: IOrderState) {
        return state;
    }

    @Selector()
    static getOrders(state: IOrderState): IOrder[] {
        return [...state.orders];
    }

    @Action(GetOrders)
    getOrders({ getState, setState }: StateContext<IOrderState>, payload: { pageModel: IOrderPage }): Observable<IResultPageModel> {
        return this.httpService.postGetOrders(payload.pageModel).pipe(
            tap(result => {
                const state = getState();
                setState({
                    ...state,
                    orders: result.elements,
                    pageModel: {
                        ...state.pageModel,
                        pageOptions: result.pageOptions,
                    }
                });
            })
        );
    }

    @Action(UpdateOrder)
    updateOrder({ getState, setState }: StateContext<IOrderState>, payload: { orderId: number, status: EStatusType }) {
        const state = getState();
        const orders = [...state.orders];
        const index = orders.findIndex(item => item.id === payload.orderId);
        orders[index] = { ...orders[index], status: payload.status };

        setState({
            ...state,
            orders: orders,
        });
    }
}
