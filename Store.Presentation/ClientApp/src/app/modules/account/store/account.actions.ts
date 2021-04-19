import { IUser } from "../../shared/models/IUser.model";
import { ILogin } from "../../shared/models/ILogin.model";
import { IConfirm } from "../../shared/models/IConfirm.model";
import { IToken } from "../../shared/models/IToken.model";
import { IExternalAuth } from "../../shared/models/IExternalAuth.model";

export enum EAccountActions {
    SignIn = '[User] Sign In',
    SignUp = '[User] Sign Up',
    PasswordRecovery = '[User] Password Recovery',
    ConfirmEmail = '[User] ConfirmEmail',
    RefreshToken = '[User] RefreshToken',
    GetUser = '[User] Get User',
    EditUser = '[User] Edit User',
    SignInByGoogle = '[User] SignInByGoogle',
}

export class SignIn {
    static readonly type = EAccountActions.SignIn;
    constructor(public loginModel: ILogin, public remember: boolean) { }
};
export class SignUp {
    static readonly type = EAccountActions.SignUp;
    constructor(public user: IUser) { }
};
export class ConfirmEmail {
    static readonly type = EAccountActions.ConfirmEmail;
    constructor(public model: IConfirm) { }
};
export class PasswordRecovery {
    static readonly type = EAccountActions.PasswordRecovery;
    constructor(public email: string) { }
};
export class RefreshToken {
    static readonly type = EAccountActions.RefreshToken;
    constructor(public token: IToken) { }
};
export class GetUser {
    static readonly type = EAccountActions.GetUser;
    constructor(public userId: string) { }
};
export class EditUser {
    static readonly type = EAccountActions.EditUser;
    constructor(public user: IUser) { }
};
export class SignInByGoogle {
    static readonly type = EAccountActions.SignInByGoogle;
    constructor(public externalAuth: IExternalAuth, public remember: boolean) { }
}