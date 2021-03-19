import { createAction, props } from "@ngrx/store";
import { IUser } from "../../shared/models/IUser.model";
import { ILogin } from "../../shared/models/ILogin.model";
import { IConfirm } from "../../shared/models/IConfirm.model";

export enum EAccountActions {
    SignIn = '[User] Sign In',
    SignInSuccess = '[User] Sign In Success',
    SignUp = '[User] Sign Up',
    SignUpSuccess = '[User] Sign Up Success',
    PasswordRecovery = '[User] Password Recovery',
    PasswordRecoverySuccess = '[User] Password Recovery Success',
    ConfirmEmail = '[User] ConfirmEmail',
    ConfirmEmailSuccess = '[User] ConfirmEmailSuccess',
    RefreshToken = '[User] RefreshToken',
    RefreshTokenSuccess = '[User] RefreshTokenSuccess',
    GetUser = '[User] Get User',
    GetUserSuccess = '[User] Get User Success',
    EditUser = '[User] Edit User',
    EditUserSucces = '[User] Edit User Success',
}

export const signIn = createAction(EAccountActions.SignIn, props<{ loginModel: ILogin, remember:boolean }>());
export const signInSuccess = createAction(EAccountActions.SignInSuccess, props<{
    accessToken: string, refreshToken: string;
}>());
export const signUp = createAction(EAccountActions.SignUp, props<{ user: IUser; }>());
export const signUpSuccess = createAction(EAccountActions.SignUpSuccess, props<{ user: IUser; }>());
export const confirmEmail = createAction(EAccountActions.ConfirmEmail, props<{ confirmModel: IConfirm; }>());
export const confirmEmailSuccess = createAction(EAccountActions.ConfirmEmailSuccess, props<{ user: IUser; }>());
export const passwordRecovery = createAction(EAccountActions.PasswordRecovery, props<{ email: string; }>());
export const passwordRecoverySuccess = createAction(EAccountActions.PasswordRecovery, props<{ result: string; }>());
export const refreshToken = createAction(EAccountActions.RefreshToken, props<{ accessToken: string, refreshToken: string; }>());
export const refreshTokenSuccess = createAction(EAccountActions.RefreshTokenSuccess, props<{ accessToken: string, refreshToken: string; }>());
export const getUser = createAction(EAccountActions.GetUser, props<{ userId: string }>());
export const getUserSuccess = createAction(EAccountActions.GetUserSuccess, props<{ user: IUser }>());
export const editUser = createAction(EAccountActions.EditUser, props<{ user: IUser }>());
export const editUserSuccess = createAction(EAccountActions.EditUserSucces, props<{ result: boolean }>());