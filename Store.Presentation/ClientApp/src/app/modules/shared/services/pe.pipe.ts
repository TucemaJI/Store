import { Pipe, PipeTransform } from "@angular/core";
import { PrintingEditionLabel } from "../models/printing-edition-type.enum";

@Pipe({ name: 'pe' })

export class PEPipe implements PipeTransform{
    transform(value: number, args?: any[]):string {
        return PrintingEditionLabel.get(value);
    }
}