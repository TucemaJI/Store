import { Action, createAction, props } from "@ngrx/store";
import { User } from "../models/User";
import { ILoginModel } from "../models/ILoginModel";

export enum EAccountActions {
    SignIn = '[User] Sign In',
    SignInSuccess = '[User] Sign In Success',
    SignUp = '[User] Sign Up',
    SignUpSuccess = '[User] Sign Up Success',
    PasswordRecovery = '[User] Password Recovery',
    PasswordRecoverySuccess = '[User] Password Recovery Success',

}

export const signIn = createAction(EAccountActions.SignIn, props<{
    loginModel: ILoginModel;
}>());
export const signInSuccess = createAction(EAccountActions.SignInSuccess, props<{
    accessToken: string, refreshToken: string;
}>());
export const signUp = createAction(EAccountActions.SignUp, props<{
    user: User;
}>());
export const signUpSuccess = createAction(EAccountActions.SignUpSuccess, props<{
    resp: boolean;
}>());
export const passwordRecovery = createAction(EAccountActions.PasswordRecovery, props<{
    email: string;
}>());
export const passwordRecoverySuccess = createAction(EAccountActions.PasswordRecovery, props<{
    email: string;
}>());

// export class SignIn implements Action {
//     public readonly type = EAccountActions.SignIn;
// }
// export class SignInSuccess implements Action {
//     public readonly type = EAccountActions.SignInSuccess;
// }
// export class SignUp implements Action {
//     public readonly type = EAccountActions.SignUp;
// }
// export class SignUpSuccess implements Action {
//     public readonly type = EAccountActions.SignUpSuccess;
// }
// export class PasswordRecovery implements Action {
//     public readonly type = EAccountActions.PasswordRecovery;
// }
// export class PasswordRecoverySuccess implements Action {
//     public readonly type = EAccountActions.PasswordRecoverySuccess;
// }

//export type AccountActions = SignIn | SignInSuccess | SignUp | SignUpSuccess | PasswordRecovery | PasswordRecoverySuccess;