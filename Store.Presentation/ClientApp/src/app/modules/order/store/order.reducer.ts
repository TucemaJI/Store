import { Action, createReducer, on } from "@ngrx/store";
import * as OrderActions from "./order.action";
import { initialOrderState, IOrderState } from "./order.state";

const createOrderReducer = createReducer(
    initialOrderState,
    on(OrderActions.getOrdersSuccess, (state, { pageOptions, orders }) => ({ ...state, orders: orders, pageModel: { ...state.pageModel, pageOptions: pageOptions, } })),
)

export const orderReducer = (state = initialOrderState, action: Action): IOrderState => {
    return createOrderReducer(state, action);
}
export const reducerKey = 'order';