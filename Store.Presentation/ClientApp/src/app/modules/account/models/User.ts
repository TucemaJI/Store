export class User {
    firstName: string;
    lastName: string;
    email: string;
    password: string;
    confirmPassword: string;
    accessToken: string;
    refreshToken: string;
    confirmed: boolean;

    constructor(model: User) {
        this.firstName = model.firstName;
        this.lastName = model.lastName;
        this.email = model.email;
        this.password = model.password;
        this.confirmPassword = model.confirmPassword;
        this.accessToken = model.accessToken;
        this.refreshToken = model.refreshToken;
        this.confirmed = model.confirmed;
    }
}