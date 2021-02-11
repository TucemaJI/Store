import { routerReducer } from "@ngrx/router-store";
import { ActionReducerMap } from "@ngrx/store";
import { IAppState } from "../state/app.state";
import { accountReducer }  from "../../modules/account/store/account.reducer"
import { administratorReducer } from "src/app/modules/administrator/store/administrator.reducer";

export const appReducers: ActionReducerMap<IAppState, any> = {
    router: routerReducer,
    account: accountReducer,
    administrator: administratorReducer,
}