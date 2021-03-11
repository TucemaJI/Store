import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { select, Store } from '@ngrx/store';
import { BaseCartItem } from 'ng-shopping-cart';
import { IPrintingEdition } from 'src/app/modules/shared/models/IPrintingEdition';
import { ShoppingCartService } from 'src/app/modules/shared/services/shopping-cart.service';
import { IAppState } from 'src/app/store/state/app.state';
import { getPE } from '../../store/printing-edition.actions';
import { selectPrintingEditions, selectUser } from '../../store/printing-edition.selector';

@Component({
  selector: 'app-select-pe',
  templateUrl: './select-pe.component.html',
  styleUrls: ['./select-pe.component.css']
})
export class SelectPEComponent implements OnInit {

  count: number = 1;
  printingEdition: IPrintingEdition;
  userId: string;

  constructor(private route: ActivatedRoute, private store: Store<IAppState>, private cartService: ShoppingCartService<BaseCartItem>, private router: Router) { }
  ngOnInit() {
    const idStr = this.route.snapshot.paramMap.get('id');
    const id = Number.parseInt(idStr);
    debugger;
    this.selectPE(id);
    if (this.printingEdition === undefined) {
      this.store.dispatch(getPE({ id: id }));
      debugger;
    }
  }

  // selectPE$ = this.store.pipe(select(selectPrintingEditions)).subscribe(
  //   data => {
  //     debugger;
  //     if (data.printingEditions !== null) {
  //       debugger;
  //       this.printingEdition = data.printingEditions.find(el => el.id === this.id);
  //     }
  //   }
  // );

  add() {
    if (this.userId === undefined){
      this.router.navigateByUrl("sign-in");
    }
    if (this.count < 1) {
      alert("Quantity cannot be less 1")
      return;
    }
    
    const item = new BaseCartItem({ id: this.printingEdition.id, name: this.printingEdition.title, price: this.printingEdition.price, quantity: this.count, data: this.printingEdition.currencyType });

    if (this.cartService.isExist(this.printingEdition.id.toString())) {
      let itemFromCookie = this.cartService.getItem(this.printingEdition.id);
      itemFromCookie.quantity = itemFromCookie.quantity + this.count;
      debugger;
      this.cartService.addItem(itemFromCookie);
      return;
    }
    this.cartService.addItem(item);

  }

  selectUser() {
    this.store.pipe(select(selectUser)).subscribe(
      data => {
        if (data !== null) {
          debugger;
          this.userId = data.id;
        }
      }
    );
  }

  selectPE(id: number) {
    this.store.pipe(select(selectPrintingEditions)).subscribe(
      data => {
        if (data.printingEditions !== null) {
          debugger;
          this.printingEdition = data.printingEditions.find(el => el.id === id);
        }
      }
    );
  }


}