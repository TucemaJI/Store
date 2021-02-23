import { createAction, props, } from "@ngrx/store";

export enum EErrorActions {
    Error = '[Error]',
}

export const error = createAction(EErrorActions.Error, props<{ err: any; }>());
