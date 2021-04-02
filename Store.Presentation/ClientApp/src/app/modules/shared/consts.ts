import { environment } from "src/environments/environment";

export class Consts {
    public static AUTHOR_COLUMNS = ['ID', 'Name', 'Product'];
    public static NAME = 'name';
    public static ID = 'id';

    public static CLIENTS_COLUMNS = ['userName', 'userEmail', 'status', 'buttons'];
    public static AUTHOR_PAGE_PARAMETERS = {
        itemsPerPage: 10,
        currentPage: 1,
        totalItems: 0,
    };
    public static CLIENTS_PAGE_PARAMETERS = {
        itemsPerPage: 10,
        currentPage: 1,
        totalItems: 0,
    };
    public static FILTER_USERNAME = 'userName';
    public static FILTER_EMAIL = 'email';
    public static CART_COLUMNS = ['product', 'unitPrice', 'count', 'amount'];
    public static ORDERS_COLUMNS = ['Order ID', 'Order time', 'Product', 'Title', 'Qty', 'Order amount', 'Order Status'];
    public static ORDERS_PAGE_PARAMETERS = {
        itemsPerPage: 10,
        currentPage: 1,
        totalItems: 0,
    }
    public static PE_OPTIONS = {
        floor: 0,
        ceil: 0,
    }
    public static SORT_LOW_TO_HIGH = 'Price: Low to High';
    public static SORT_HIGH_TO_LOW = 'Price: High to Low';
    public static PE_PAGE_PARAMETERS = {
        itemsPerPage: 6,
        currentPage: 1,
        totalItems: 0,
    }
    public static ROUTE_SIGN_IN = 'sign-in';
    public static ROUTE_CONFIRM_EMAIL = "confirm-email";
    public static ROUTE_SIGN_UP = "sign-up";
    public static ROUTE_PASSWORD_RECOVERY = "password-recovery";
    public static ROUTE_PROFILE = "profile";
    public static ROUTE_PRINTING_EDITIONS = "printing-edition";
    public static ROUTE_PRINTING_EDITION = "pe/:id";
    public static ROUTE_ORDERS = "orders";
    
    public static FORBIDDEN = "You have not pass";
    public static SORT_BY = 'Price';
    public static QUANTITY_MINIMUM = "Quantity cannot be less 1";
    public static QUANTITY_MINIMUM_VALUE = 1;
    public static CART_DIALOG_SIZE = "300%";
    public static BEARER = 'Bearer ';
    public static ACCESS_TOKEN = 'accessToken';
    public static REFRESH_TOKEN = 'refreshToken';
    public static ROLE = 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role';
    public static ADMIN = 'Admin';
    public static IDENTIFIER = 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier';
    public static CURRENCY = 'currency';
    public static ENUM_TO_ARRAY = 'enumToArray';
    public static PRINTING_EDITION_PIPE = 'printingeditionpipe';
    public static STATUS = 'status';
    public static PASSWORD = 'password';
    public static CONFIRM_PASSWORD = 'confirmPassword';
    public static FIRST_NAME = 'firstName';
    public static LAST_NAME = 'lastName';
    public static EMAIL = 'email';
    public static VALID_LETTERS = /[A-Z]/;
    public static VALID_SYMBOLS = /[ `!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?~]/;
    public static VALID_NUM = /[0-9]/;
    public static VALID_MIN_LENGTH = 6;
    public static TO_SEPARATE_ENUM = 2;
    public static EXPIRE_DAYS = 60;
    public static HIGH_VALUE = 1;
    public static SIGN_IN = `${environment.apiURL}${environment.accountC}/signin`;
    public static REGISTRATION = `${environment.apiURL}${environment.accountC}/registration`;
    public static FORGOT_PASSWORD = `${environment.apiURL}${environment.accountC}/ForgotPassword?email=`;
    public static CHECKMAIL = `${environment.apiURL}${environment.accountC}/checkmail`;
    public static POST_REFRESH_TOKEN = `${environment.apiURL}${environment.accountC}/refreshtoken`;
    public static GET_USER = `${environment.apiURL}${environment.userC}/getuser?id=`;
    public static UPDATE_USER = `${environment.apiURL}${environment.userC}/updateuser`;
    public static FILTER_USERS = `${environment.apiURL}${environment.userC}/filterusers`;
    public static DELETE_USER = `${environment.apiURL}${environment.userC}/deleteuser`;
    public static GET_AUTHORS = `${environment.apiURL}${environment.authorC}/getauthorswithfilter`;
    public static CREATE_AUTHOR = `${environment.apiURL}${environment.authorC}/createauthor`;
    public static UPDATE_AUTHOR = `${environment.apiURL}${environment.authorC}/updateauthor`;
    public static DELETE_AUTHOR = `${environment.apiURL}${environment.authorC}/deleteauthor`;
    public static CREATE_ORDER = `${environment.apiURL}${environment.orderC}/createorder`;
    public static PAY = `${environment.apiURL}${environment.orderC}/pay`;
    public static GET_ORDERS = `${environment.apiURL}${environment.orderC}/getorders`;
    public static GET_PES = `${environment.apiURL}${environment.peC}/getprintingeditions`;
    public static GET_PE = `${environment.apiURL}${environment.peC}/`;


}