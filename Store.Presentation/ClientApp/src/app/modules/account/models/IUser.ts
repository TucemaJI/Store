export interface IUser{
    id:number;
    firstName:string;
    lastName:string;
    email:string;
    password:string;
    confirmPassword:string;
    accessToken:string;
    refreshToken:string;
}