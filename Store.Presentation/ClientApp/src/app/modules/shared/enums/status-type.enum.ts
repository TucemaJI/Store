export enum EStatusType{
    none = 0,
    paid = 1,
    unpaid = 2,
}

export const StatusLabel = new Map<number, string>([
    [EStatusType.paid, "Paid"],
    [EStatusType.unpaid, "Unpaid"],
]);