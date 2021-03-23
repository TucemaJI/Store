import { Pipe, PipeTransform } from "@angular/core";
import { Consts } from "../consts";
import { StatusLabel } from "../models/status-type.enum";

@Pipe({ name: Consts.STATUS })

export class StatusPipe implements PipeTransform {
    transform(value: number, args?: any[]): string {
        return StatusLabel.get(value);
    }

}