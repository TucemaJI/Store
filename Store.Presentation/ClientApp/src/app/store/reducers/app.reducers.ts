import { routerReducer } from "@ngrx/router-store";
import { ActionReducerMap } from "@ngrx/store";
import { IAppState } from "../state/app.state";
import { accountReducer }  from "../../modules/account/store/account.reducer"

export const appReducers: ActionReducerMap<IAppState, any> = {
    router: routerReducer,
    account: accountReducer,
}