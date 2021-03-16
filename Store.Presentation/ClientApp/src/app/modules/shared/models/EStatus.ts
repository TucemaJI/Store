export enum EStatus{
    none = 0,
    paid = 1,
    unpaid = 2,
}

export const StatusLabel = new Map<number, string>([
    [EStatus.paid, "Paid"],
    [EStatus.unpaid, "Unpaid"],
]);