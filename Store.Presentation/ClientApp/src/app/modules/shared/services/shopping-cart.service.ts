import { Injectable } from "@angular/core";
import { BaseCartItem } from "ng-shopping-cart";
import { CookieService } from "ngx-cookie-service";

@Injectable()
export class ShoppingCartService<T extends BaseCartItem> {
    constructor(private cookie: CookieService) { }
    getItem(id: any): T {
        const t: T = JSON.parse(this.cookie.get(id));
        return t;
    }
    getItems(): T[] {
        debugger;
        const strT = this.cookie.getAll();
        let t: T[] = new Array();
        debugger;
        for (let i in strT) {
            t.push(JSON.parse(this.cookie.get(i)));
        };
        console.log(t)
        return t;
    }
    addItem(item: T): void {
        const str = JSON.stringify(item);
        debugger;
        this.cookie.set(item.id, str, 60, '/');
    }
    removeItem(id: any): void {
        if (this.cookie.check(id)) {
            debugger;
            this.cookie.delete(id,'/');
        }
    }
    isExist(id: string): boolean {
        const t = this.cookie.get(id);
        let val: boolean;
        t === "" ? val = false : val = true;
        return val;
    }
}