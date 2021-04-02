import { Injectable } from "@angular/core";
import { BaseCartItem } from "ng-shopping-cart";
import { CookieService } from "ngx-cookie-service";
import { Subject } from "rxjs";
import { Consts } from "../consts";

@Injectable()
export class ShoppingCartService<T extends BaseCartItem> {

    cartChanged = new Subject<number>();

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
        this.cartChanged.next(this.getItems().length);
    }
    removeItem(id: any): void {
        if (this.cookie.check(id)) {
            this.cookie.delete(id,'/');
            this.cartChanged.next(this.getItems().length);
        }
    }
    clean():void{
        this.cookie.deleteAll('/');
        this.cartChanged.next(this.getItems().length);
    }
    isExist(id: string): boolean {
        const t = this.cookie.get(id);
        let val: boolean;
        t === "" ? val = false : val = true;
        return val;
    }
}