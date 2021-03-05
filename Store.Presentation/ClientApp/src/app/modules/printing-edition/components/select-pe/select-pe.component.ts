import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { select, Store } from '@ngrx/store';
import { BaseCartItem, CartService } from 'ng-shopping-cart';
import { pipe } from 'rxjs';
import { filter } from 'rxjs/operators';
import { IPrintingEdition } from 'src/app/modules/shared/models/IPrintingEdition';
import { IAppState } from 'src/app/store/state/app.state';
import { getPE, getPEs } from '../../store/printing-edition.actions';
import { selectPrintingEditions } from '../../store/printing-edition.selector';

@Component({
  selector: 'app-select-pe',
  templateUrl: './select-pe.component.html',
  styleUrls: ['./select-pe.component.css']
})
export class SelectPEComponent implements OnInit {

  id: number;
  count: number = 1;
  printingEdition: IPrintingEdition;
  test;

  constructor(private route: ActivatedRoute, private store: Store<IAppState>, private cartService: CartService<BaseCartItem>) { }
  ngOnInit() {
    const idStr = this.route.snapshot.paramMap.get('id');
    //const id = Number.parseInt(idStr);
    this.id = Number.parseInt(idStr);
    debugger;
    //this.selectPE(id);
    if (this.printingEdition === undefined) {
      this.store.dispatch(getPE({ id: this.id }));
      debugger;
    }
  }

  selectPE$ = this.store.pipe(select(selectPrintingEditions)).subscribe(
    data => {
      debugger;
      if (data.printingEditions !== null) {
        debugger;
        this.printingEdition = data.printingEditions.find(el => el.id === this.id);
      }
    }
  );
// Here: "Qty with -number" not good
// Cart in cookies
  add() {
    const item = new BaseCartItem({ id: this.printingEdition.id, name: this.printingEdition.title, price: this.printingEdition.price, quantity: this.count, data: this.printingEdition.currencyType });
    if (this.cartService.getItem(this.printingEdition.id) !== undefined) {
      this.cartService.getItem(this.printingEdition.id).setQuantity(this.cartService.getItem(this.printingEdition.id).getQuantity() + this.count);
      this.cartService.addItem(this.cartService.getItem(this.printingEdition.id));
      return;
    }
    this.cartService.addItem(item);
  }

  // selectPE(id: number) {
  //   this.store.pipe(select(selectPrintingEditions)).subscribe(
  //     data => {
  //       if (data.printingEditions !== null) {
  //         debugger;
  //         this.printingEdition = data.printingEditions.find(el => el.id === id);
  //       }
  //     }
  //   );
  // }


}