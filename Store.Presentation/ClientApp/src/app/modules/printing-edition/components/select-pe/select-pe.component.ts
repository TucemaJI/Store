import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { select, Store } from '@ngrx/store';
import { BaseCartItem } from 'ng-shopping-cart';
import { Consts } from 'src/app/modules/shared/consts';
import { IPrintingEdition } from 'src/app/modules/shared/models/IPrintingEdition.model';
import { AuthService } from 'src/app/modules/shared/services/auth.service';
import { ShoppingCartService } from 'src/app/modules/shared/services/shopping-cart.service';
import { IAppState } from 'src/app/store/state/app.state';
import { getPE } from '../../store/printing-edition.actions';
import { selectPrintingEditions } from '../../store/printing-edition.selector';

@Component({
  selector: 'app-select-pe',
  templateUrl: './select-pe.component.html',
  styleUrls: ['./select-pe.component.css']
})
export class SelectPEComponent implements OnInit {

  count: number = Consts.QUANTITY_MINIMUM_VALUE;
  printingEdition: IPrintingEdition;
  userId: string;

  constructor(private route: ActivatedRoute, private store: Store<IAppState>, private cartService: ShoppingCartService<BaseCartItem>, private router: Router, private auth: AuthService) { }
  ngOnInit() {
    this.userId = this.auth.getId();
    this.auth.userIdChanged.subscribe((id) => this.userId = id);
    const idStr = this.route.snapshot.paramMap.get(Consts.ID);
    const id = Number.parseInt(idStr);
    this.selectPE(id);
    if (this.printingEdition === undefined) {
      this.store.dispatch(getPE({ id }));
    }
  }

  add(): void {
    if (this.userId === undefined) {
      this.router.navigateByUrl(Consts.ROUTE_SIGN_IN);
    }
    if (this.count < Consts.QUANTITY_MINIMUM_VALUE) {
      alert(Consts.QUANTITY_MINIMUM)
      return;
    }

    const item = new BaseCartItem({ id: this.printingEdition.id, name: this.printingEdition.title, price: this.printingEdition.price, quantity: this.count, data: this.printingEdition.currencyType });

    if (this.cartService.isExist(this.printingEdition.id.toString())) {
      let itemFromCookie = this.cartService.getItem(this.printingEdition.id);
      itemFromCookie.quantity = itemFromCookie.quantity + this.count;
      this.cartService.addItem(itemFromCookie);
      return;
    }
    this.cartService.addItem(item);
  }

  selectPE(id: number): void {
    this.store.pipe(select(selectPrintingEditions)).subscribe(
      data => {
        if (data.printingEditions !== null) {
          this.printingEdition = data.printingEditions.find(el => el.id === id);
        }
      }
    );
  }
}