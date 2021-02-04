import { createAction, props, } from "@ngrx/store";

export const error = createAction('[ERROR]', props<{
    err: any;
}>());