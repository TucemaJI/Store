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
    public static ROUTE_SIGN_IN = 'sign-in';
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
    public static SORT_BY = 'Price';
    public static QUANTITY_MINIMUM = "Quantity cannot be less 1";
    public static QUANTITY_MINIMUM_VALUE = 1;
    public static CART_DIALOG_SIZE = "300%";
    public static BEARER = 'Bearer ';
    public static ACCESS_TOKEN = 'accessToken';
    public static REFRESH_TOKEN = 'refreshToken';
    public static SIGN_IN = `${environment.apiURL}`
}