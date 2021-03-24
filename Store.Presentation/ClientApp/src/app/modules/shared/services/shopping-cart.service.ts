import { Injectable } from "@angular/core";
import { BaseCartItem } from "ng-shopping-cart";
import { CookieService } from "ngx-cookie-service";
import { Consts } from "../consts";

@Injectable()
export class ShoppingCartService<T extends BaseCartItem> {
    constructor(private cookie: CookieService) { }
    getItem(id: any): T {
        const t: T = JSON.parse(this.cookie.get(id));
        return t;
    }
    getItems(): T[] {
        const strT = this.cookie.getAll();
        let t: T[] = new Array();
        for (let i in strT) {
            t.push(JSON.parse(this.cookie.get(i)));
        };
        return t;
    }
    addItem(item: T): void {
        const str = JSON.stringify(item);
        this.cookie.set(item.id, str, Consts.EXPIRE_DAYS, '/');
    }
    removeItem(id: any): void {
        if (this.cookie.check(id)) {
            this.cookie.delete(id,'/');
        }
    }
    clean():void{
        this.cookie.deleteAll('/');
    }
    isExist(id: string): boolean {
        const t = this.cookie.get(id);
        let val: boolean;
        t === "" ? val = false : val = true;
        return val;
    }
}