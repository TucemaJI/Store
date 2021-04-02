export interface ICartState {
    orderId: number,
    orderStatus: boolean,
}

export const initialCartState: ICartState = {
    orderId: null,
    orderStatus: null,
}