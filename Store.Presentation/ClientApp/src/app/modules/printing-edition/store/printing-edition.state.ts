import { Injectable } from "@angular/core";
import { Action, Selector, State, StateContext } from "@ngxs/store";
import { tap } from "rxjs/operators";
import { IPrintingEdition } from "../../shared/models/IPrintingEdition.model";
import { IPrintingEditionPage } from "../../shared/models/IPrintingEditionPage.model";
import { PrintingEditionHttpService } from "../../shared/services/printing-edition-http.service";
import { GetPE, GetPEs } from "./printing-edition.actions";

export interface IPrintingEditionState {
    printingEditions: IPrintingEdition[];
    pageModel: IPrintingEditionPage;
}

@State<IPrintingEditionState>({
    name: 'printingEdition',
    defaults: {
        printingEditions: null,
        pageModel: null,
    }
})

@Injectable()
export class PrintingEditionsState {
    constructor(private httpService: PrintingEditionHttpService) { }

    @Selector()
    static getState(state: IPrintingEditionState) {
        return state;
    }

    @Action(GetPEs)
    getPEs({ getState, setState }: StateContext<IPrintingEditionState>, payload: { pageModel: IPrintingEditionPage }) {
        return this.httpService.postPE(payload.pageModel).pipe(
            tap(result => {
                const state = getState();
                setState({
                    ...state,
                    printingEditions: result.elements,
                    pageModel: {
                        ...state.pageModel,
                        pageOptions: result.pageOptions,
                        maxPrice: result.maxPrice,
                        minPrice: result.minPrice,
                    }
                });
            })
        );
    }

    @Action(GetPE)
    getPE({ getState, setState }: StateContext<IPrintingEditionState>, payload: { id: number }) {
        return this.httpService.getPE(payload.id).pipe(
            tap(result => {
                const state = getState();
                setState({
                    ...state,
                    printingEditions: [result],
                });
            })
        );
    }
}