import { Pipe, PipeTransform } from "@angular/core";
import { Consts } from "../consts";
import { CurrencyLabel } from "../enums/currency-type.enum";

@Pipe({ name:  Consts.CURRENCY})

export class CurrencyPipe implements PipeTransform {
    transform(value: number, args?: any[]): string {
        return CurrencyLabel.get(value);
    }

}