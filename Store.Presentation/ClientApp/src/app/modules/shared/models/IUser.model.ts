export interface IUser {
    firstName: string;
    lastName: string;
    email: string;
    password: string;
    confirmPassword: string;
    accessToken: string;
    refreshToken: string;
    confirmed: boolean;
    id: string;
}