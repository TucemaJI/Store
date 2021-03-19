export enum EPrintingEditionType {
    none = 0,
    book = 1,
    journal = 2,
    newspaper = 3,
}

export const PrintingEditionLabel = new Map<number, string>([
    [EPrintingEditionType.book, "Book"],
    [EPrintingEditionType.journal, "Journal"],
    [EPrintingEditionType.newspaper, "Newspaper"],
]);