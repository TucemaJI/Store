export enum ECurrencyType {
    none = 0,
    USD = 1,
    EUR = 2,
    GBP = 3,
    CHF = 4,
    JPY = 5,
    RUB = 6,
}

export const CurrencyLabel = new Map<number, string>([
    [ECurrencyType.USD, "USD"],
    [ECurrencyType.EUR, "EUR"],
    [ECurrencyType.GBP, "GBP"],
    [ECurrencyType.CHF, "CHF"],
    [ECurrencyType.JPY, "JPY"],
    [ECurrencyType.RUB, "RUB"],
])