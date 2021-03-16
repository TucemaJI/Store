import { createSelector } from "@ngrx/store";
import { IAppState } from "src/app/store/state/app.state";
import { IOrderState } from "./order.state";

const getState = (state: IAppState) => state.order;

export const selectOrders = createSelector(
    getState,
    (state: IOrderState) => state,
)