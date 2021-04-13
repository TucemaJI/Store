import { IUser } from "../../shared/models/IUser.model";
import { ILogin } from "../../shared/models/ILogin.model";
import { IConfirm } from "../../shared/models/IConfirm.model";
import { IToken } from "../../shared/models/IToken.model";

export enum EAccountActions {
    SignIn = '[User] Sign In',
    SignUp = '[User] Sign Up',
    PasswordRecovery = '[User] Password Recovery',
    ConfirmEmail = '[User] ConfirmEmail',
    RefreshToken = '[User] RefreshToken',
    GetUser = '[User] Get User',
    EditUser = '[User] Edit User',
}

export class SignIn {
    static readonly type = EAccountActions.SignIn;
    constructor(public payload: { loginModel: ILogin, remember: boolean }) { }
};
export class SignUp {
    static readonly type = EAccountActions.SignUp;
    constructor(public payload: { user: IUser }) { }
};
export class ConfirmEmail {
    static readonly type = EAccountActions.ConfirmEmail;
    constructor(public payload: { model: IConfirm }) { }
};
export class PasswordRecovery {
    static readonly type = EAccountActions.PasswordRecovery;
    constructor(public payload: { email: string }) { }
};
export class RefreshToken {
    static readonly type = EAccountActions.RefreshToken;
    constructor(public payload: { token: IToken }) { }
};
export class GetUser {
    static readonly type = EAccountActions.GetUser;
    constructor(public payload: { userId: string }) { }
};
export class EditUser {
    static readonly type = EAccountActions.EditUser;
    constructor(public payload: { user: IUser }) { }
};