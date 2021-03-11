import { RouterReducerState } from "@ngrx/router-store";
import { initialUserState, IAccountState } from "src/app/modules/account/store/account.state";
import { IAdministratorState, initialAdministratorState } from "src/app/modules/administrator/store/administrator.state";
import { ICartState, initialCartState } from "src/app/modules/cart/store/cart.state";
import { initialPrintingEditionsState, IPrintingEditionState } from "src/app/modules/printing-edition/store/printing-edition.state";

export interface IAppState {
    router?: RouterReducerState;
    account: IAccountState;
    administrator: IAdministratorState;
    printingEditions: IPrintingEditionState;
    cart: ICartState;
}

export const initialAppState: IAppState = {
    account: initialUserState,
    administrator: initialAdministratorState,
    printingEditions: initialPrintingEditionsState,
    cart: initialCartState,
}

export function getInitialState(): IAppState {
    return initialAppState;
}