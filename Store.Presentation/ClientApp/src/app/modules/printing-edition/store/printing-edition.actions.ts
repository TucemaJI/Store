import { IPrintingEditionPage } from "../../shared/models/IPrintingEditionPage.model";

export enum EPrintingEditionActions {
    GetPEs = '[PrintingEdition] Get Printing Editions',
    GetPE = '[PrintingEdition] Get Printing Edition',
}

export class GetPEs {
    static readonly type = EPrintingEditionActions.GetPEs;
    constructor(public pageModel: IPrintingEditionPage ) { }
};
export class GetPE {
    static readonly type = EPrintingEditionActions.GetPE;
    constructor(public  id: number ) { }
};