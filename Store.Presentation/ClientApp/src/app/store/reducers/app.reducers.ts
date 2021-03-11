import { routerReducer } from "@ngrx/router-store";
import { ActionReducerMap } from "@ngrx/store";
import { IAppState } from "../state/app.state";
import { accountReducer }  from "../../modules/account/store/account.reducer"
import { administratorReducer } from "src/app/modules/administrator/store/administrator.reducer";
import { printingEditionReducer } from "src/app/modules/printing-edition/store/printing-edition.reducer";
import { cartReducer } from "src/app/modules/cart/store/cart.reducer";

export const appReducers: ActionReducerMap<IAppState, any> = {
    router: routerReducer,
    account: accountReducer,
    administrator: administratorReducer,
    printingEditions: printingEditionReducer,
    cart: cartReducer,
}