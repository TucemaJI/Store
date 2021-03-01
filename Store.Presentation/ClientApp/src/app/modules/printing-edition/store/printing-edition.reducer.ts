import { Action, createReducer, on } from "@ngrx/store";
import * as PrintingEditionActions from "./printing-edition.actions";
import { initialPrintingEditionsState, IPrintingEditionState } from "./printing-edition.state";

const createPrintingEditionReducer = createReducer(
    initialPrintingEditionsState,
    on(PrintingEditionActions.getPESuccess, (state, { pageParameters, printingEditions }) => ({ ...state, printingEditions: printingEditions, pageModel: { ...state.pageModel, pageParameters: pageParameters, } })),
    
)

export const printingEditionReducer = (state = initialPrintingEditionsState, action: Action): IPrintingEditionState => {
    return createPrintingEditionReducer(state, action);
}
export const reducerKey = 'printingEditions';