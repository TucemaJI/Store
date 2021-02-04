import { routerReducer } from "@ngrx/router-store";
import { ActionReducerMap } from "@ngrx/store";
import { IAppState } from "../state/app.state";
import { reducer }  from "../../modules/account/store/account.reducer"

export const appReducers: ActionReducerMap<IAppState, any> = {
    router: routerReducer,
    user: reducer
}