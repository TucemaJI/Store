import { Pipe, PipeTransform } from "@angular/core";
import { Consts } from "../consts";

@Pipe({ name:  Consts.ENUM_TO_ARRAY})

export class EnumToArrayPipe implements PipeTransform {
    transform(data: Object) {
        const keys = Object.keys(data);
        return keys.slice(keys.length / Consts.TO_SEPARATE_ENUM);
    }
}