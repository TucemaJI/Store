import { createSelector } from "@ngrx/store";
import { IAppState } from "src/app/store/state/app.state";
import { ICartState } from "./cart.state";

const getState = (state: IAppState) => state.cart;

export const selectCartState = createSelector(
    getState,
    (state: ICartState) => state,
)