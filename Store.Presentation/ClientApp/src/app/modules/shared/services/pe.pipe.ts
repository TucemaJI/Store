import { Pipe, PipeTransform } from "@angular/core";
import { Consts } from "../consts";
import { PrintingEditionLabel } from "../models/printing-edition-type.enum";

@Pipe({ name: Consts.PE })

export class PEPipe implements PipeTransform {
    transform(value: number, args?: any[]): string {
        return PrintingEditionLabel.get(value);
    }
}