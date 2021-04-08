import { routerReducer } from "@ngrx/router-store";
import { ActionReducerMap } from "@ngrx/store";
import { IAppState } from "../state/app.state";
import { accountReducer }  from "../../modules/account/store/account.reducer"
import { printingEditionReducer } from "src/app/modules/printing-edition/store/printing-edition.reducer";
import { cartReducer } from "src/app/modules/cart/store/cart.reducer";
import { orderReducer } from "src/app/modules/order/store/order.reducer";

export const appReducers: ActionReducerMap<IAppState, any> = {
    router: routerReducer,
    account: accountReducer,
    printingEditions: printingEditionReducer,
    cart: cartReducer,
    order: orderReducer,
}