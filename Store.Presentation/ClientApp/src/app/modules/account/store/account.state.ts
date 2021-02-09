import { User } from "../models/User";

export interface IAccountState {
    user: User

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
    }
}