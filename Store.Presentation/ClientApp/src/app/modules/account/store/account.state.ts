import { IUser } from "../../shared/models/IUser.model";

export interface IAccountState {
  user: IUser
}

export const initialUserState: IAccountState = {
  user: null
}
