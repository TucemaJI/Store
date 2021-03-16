import { Pipe, PipeTransform } from "@angular/core";
import { StatusLabel } from "../models/EStatus";

@Pipe({ name: 'status' })

export class StatusPipe implements PipeTransform {
    transform(value: number, args?: any[]): string {
        return StatusLabel.get(value);
    }

}