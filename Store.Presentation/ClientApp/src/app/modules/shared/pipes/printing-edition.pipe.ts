import { Pipe, PipeTransform } from "@angular/core";
import { Consts } from "../consts";
import { PrintingEditionLabel } from "../enums/printing-edition-type.enum";

@Pipe({ name: Consts.PRINTING_EDITION_PIPE })

export class PrintiongEditionPipe implements PipeTransform {
    transform(value: number, args?: any[]): string {
        return PrintingEditionLabel.get(value);
    }
}