export interface IPageModel {
    pageParameters: {
        pageNumber: number;
        pageSize: number;
    };
    isDescending: boolean;
    orderByString: string;
    name: string;
    email: string;
}