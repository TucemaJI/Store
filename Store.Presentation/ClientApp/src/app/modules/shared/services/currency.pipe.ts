import { Pipe, PipeTransform } from "@angular/core";
import { CurrencyLabel } from "../models/currency-type.enum";

@Pipe({ name: 'currency' })

export class CurrencyPipe implements PipeTransform {
    transform(value: number, args?: any[]): string {
        return CurrencyLabel.get(value);
    }

}