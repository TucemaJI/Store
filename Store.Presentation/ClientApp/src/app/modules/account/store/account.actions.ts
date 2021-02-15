import { Action, createAction, props } from "@ngrx/store";
import { User } from "../models/User";
import { ILoginModel } from "../models/ILoginModel";
import { IConfirmModel } from "../models/IConfirmModel";
import { Token } from "../models/Token";

export enum EAccountActions {
    SignIn = '[User] Sign In',
    SignInSuccess = '[User] Sign In Success',
    SignUp = '[User] Sign Up',
    SignUpSuccess = '[User] Sign Up Success',
    PasswordRecovery = '[User] Password Recovery',
    PasswordRecoverySuccess = '[User] Password Recovery Success',
    ConfirmPassword = '[User] Confirm',
    ConfirmPasswordSuccess = '[User] ConfirmSuccess',
    RefreshToken = '[User] RefreshToken',
    RefreshTokenSuccess = '[User] RefreshTokenSuccess'
}

export const signIn = createAction(EAccountActions.SignIn, props<{ loginModel: ILoginModel; }>());
export const signInSuccess = createAction(EAccountActions.SignInSuccess, props<{
    accessToken: string, refreshToken: string;
}>());
export const signUp = createAction(EAccountActions.SignUp, props<{ user: User; }>());
export const signUpSuccess = createAction(EAccountActions.SignUpSuccess, props<{ user: User; }>());
export const confirmPassword = createAction(EAccountActions.ConfirmPassword, props<{ confirmModel: IConfirmModel; }>());
export const confirmPasswordSuccess = createAction(EAccountActions.ConfirmPasswordSuccess, props<{ user: User; }>());
export const passwordRecovery = createAction(EAccountActions.PasswordRecovery, props<{ email: string; }>());
export const passwordRecoverySuccess = createAction(EAccountActions.PasswordRecovery, props<{ email: string; }>());
export const refreshToken = createAction(EAccountActions.RefreshToken, props<{ accessToken: string, refreshToken: string; }>());
export const refreshTokenSuccess = createAction(EAccountActions.RefreshTokenSuccess, props<{ accessToken: string, refreshToken: string; }>())
