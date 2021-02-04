import { IUser } from "../models/IUser";

export interface IUserState {
    user: IUser;

}

export const initialUserState: IUserState = {
    user: null,
}