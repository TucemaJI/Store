import { Action, createReducer, on } from "@ngrx/store";
import { ICartState, initialCartState } from "./cart.state";
import * as CartActions from "./cart.actions";

const createCartReducer = createReducer(
    initialCartState,
    on(CartActions.createOrderSuccess, (state, { orderId }) => ({ ...state, orderId: orderId })),
    on(CartActions.paySuccess, (state, { result }) => ({ ...state, orderStatus: result })),
)

export const cartReducer = (state = initialCartState, action: Action): ICartState => {
    return createCartReducer(state, action);
}
export const reducerKey = 'cart';