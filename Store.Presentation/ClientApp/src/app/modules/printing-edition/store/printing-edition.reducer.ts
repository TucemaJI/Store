import { Action, createReducer, on } from "@ngrx/store";
import * as PrintingEditionActions from "./printing-edition.actions";
import { initialPrintingEditionsState, IPrintingEditionState } from "./printing-edition.state";

const createPrintingEditionReducer = createReducer(
    initialPrintingEditionsState,
    on(PrintingEditionActions.getPESuccess, (state, { maxPrice, pageParameters, printingEditions }) => ({ ...state, printingEditions: printingEditions, pageModel: { ...state.pageModel, pageParameters: pageParameters, maxPrice: maxPrice } })),
    on(PrintingEditionActions.getMaxPriceSuccess, (state, { maxPrice }) => ({ ...state, pageModel: { ...state.pageModel, maxPrice: maxPrice } })),
)

export const printingEditionReducer = (state = initialPrintingEditionsState, action: Action): IPrintingEditionState => {
    return createPrintingEditionReducer(state, action);
}
export const reducerKey = 'printingEditions';