import { IUser } from "../../shared/models/IUser.model";

export interface IAccountState {
    user: IUser
}

export const initialUserState: IAccountState = {
    user: {
        firstName: null,
        lastName: null,
        email: null,
        password: null,
        confirmPassword: null,
        accessToken: null,
        refreshToken: null,
        confirmed: null,
        id: null,
    }
}