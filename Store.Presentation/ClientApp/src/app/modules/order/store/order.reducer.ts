import { Action, createReducer, on } from "@ngrx/store";
import * as OrderActions from "./order.action";
import { initialOrderState, IOrderState } from "./order.state";

const createOrderReducer = createReducer(
    initialOrderState,
    on(OrderActions.getOrderSuccess, (state, { pageParameters, orders }) => ({ ...state, orders: orders, pageModel: { ...state.pageModel, pageParameters: pageParameters, } })),
)

export const orderReducer = (state = initialOrderState, action: Action): IOrderState => {
    return createOrderReducer(state, action);
}
export const reducerKey = 'order';